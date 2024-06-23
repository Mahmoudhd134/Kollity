using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Question.Edit;

public class EditExamQuestionCommandHandler : ICommandHandler<EditExamQuestionCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;

    public EditExamQuestionCommandHandler(ExamsDbContext context, IUserServices userServices, IMapper mapper,EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(EditExamQuestionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            questionId = request.Dto.Id;

        if (request.Dto.Degree <= 0)
            return ExamErrors.DegreeOutOfRange;

        if (request.Dto.OpenForSeconds < 45)
            return ExamErrors.QuestionMustAtLeast45Second;

        var exam = await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .Select(x => new
            {
                x.Exam.Room.DoctorId,
                x.Exam.StartDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.QuestionNotFound(questionId);
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;

        var question = await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .FirstOrDefaultAsync(cancellationToken);

        _mapper.Map(request.Dto, question);
        await _context.SaveChangesAsync(cancellationToken);
        _eventCollection.Raise(new ExamQuestionEditedEvent(question));
        return Result.Success();
    }
}