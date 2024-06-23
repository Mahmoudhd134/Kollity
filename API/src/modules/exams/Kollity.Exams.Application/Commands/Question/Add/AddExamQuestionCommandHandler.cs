using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Dtos.Exam;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Kollity.Exams.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Question.Add;

public class AddExamQuestionCommandHandler : ICommandHandler<AddExamQuestionCommand, ExamQuestionDto>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;

    public AddExamQuestionCommandHandler(ExamsDbContext context, IUserServices userServices, IMapper mapper,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
        _eventCollection = eventCollection;
    }

    public async Task<Result<ExamQuestionDto>> Handle(AddExamQuestionCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            examId = request.ExamId;

        if (request.Dto.Degree <= 0)
            return ExamErrors.DegreeOutOfRange;

        if (request.Dto.OpenForSeconds < 45)
            return ExamErrors.QuestionMustAtLeast45Second;

        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new
            {
                x.Room.DoctorId,
                x.StartDate,
                x.EndDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;

        var question = _mapper.Map<ExamQuestion>(request.Dto);
        question.ExamId = examId;
        var option = new ExamQuestionOption()
        {
            Option =
                "Temporally option, make another option, mark it as the right one, then remove this option.\nThis is for ensuring that at leaset one option will be present.",
            IsRightOption = true,
        };
        question.ExamQuestionOptions.Add(option);
        _context.ExamQuestions.Add(question);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new ExamQuestionAddedEvent(question));
        return _mapper.Map<ExamQuestionDto>(question);
    }
}