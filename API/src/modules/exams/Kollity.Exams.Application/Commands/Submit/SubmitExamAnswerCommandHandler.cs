using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Commands.Submit;

public class SubmitExamAnswerCommandHandler : ICommandHandler<SubmitExamAnswerCommand>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public SubmitExamAnswerCommandHandler(ExamsDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(SubmitExamAnswerCommand request, CancellationToken cancellationToken)
    {
        Guid optionId = request.OptionId,
            questionId = request.QuestionId,
            userId = _userServices.GetCurrentUserId();

        var utcNow = DateTime.UtcNow;


        var optionIsAssociatedWithQuestion = await _context.ExamQuestionOptions
            .AnyAsync(x => x.Id == optionId && x.ExamQuestionId == questionId, cancellationToken);
        if (optionIsAssociatedWithQuestion == false)
            return ExamErrors.QuestionHanNoOptionWithId(optionId);

        var answer = await _context.ExamAnswers
            .Where(x => x.ExamQuestionId == questionId && x.StudentId == userId)
            .Include(x => x.Exam)
            .Include(x => x.ExamQuestion)
            .FirstOrDefaultAsync(cancellationToken);

        if (answer is null)
            return ExamErrors.AnswerNotFound;

        if (answer.SubmitTime is not null)
            return ExamErrors.QuestionAlreadyAnswered;

        if (utcNow > answer.Exam.EndDate)
            return ExamErrors.ExamEnded;

        var lastDate = answer.RequestTime.AddSeconds(answer.ExamQuestion.OpenForSeconds);
        if (utcNow > lastDate)
            return ExamErrors.QuestionTimeEnd;

        answer.ExamQuestionOptionId = optionId;
        answer.SubmitTime = utcNow;
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new ExamAnswerSubmittedEvent(answer));
        return Result.Success();
    }
}