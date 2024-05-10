using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Domain.ExamModels;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Exam.Question.Option.Add;

public class AddExamQuestionOptionCommandHandler : ICommandHandler<AddExamQuestionOptionCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;

    public AddExamQuestionOptionCommandHandler(ApplicationDbContext context, IUserServices userServices, IMapper mapper,EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _mapper = mapper;
        _eventCollection = eventCollection;
    }

    public async Task<Result<Guid>> Handle(AddExamQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            questionId = request.QuestionId;

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
        if (exam.DoctorId == null)
            return RoomErrors.RoomHasNoDoctor;
        if (exam.DoctorId != userId)
            return ExamErrors.UnAuthorizeAction;
        if (DateTime.UtcNow >= exam.StartDate)
            return ExamErrors.CanNotEditExamAfterItStarts;

        var option = _mapper.Map<ExamQuestionOption>(request.Dto);
        option.ExamQuestionId = questionId;
        _context.ExamQuestionOptions.Add(option);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new ExamQuestionOptionAddedEvent(option));
        return option.Id;
    }
}