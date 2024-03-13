using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Submit;

public class SubmitExamAnswerCommandHandler : ICommandHandler<SubmitExamAnswerCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public SubmitExamAnswerCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
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
            .Select(x => new
            {
                x.Id,
                x.RequestTime,
                x.SubmitTime,
                ExamEndDate = x.Exam.EndDate,
                QuestionOpenForSeconds = x.ExamQuestion.OpenForSeconds
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (answer is null)
            return ExamErrors.AnswerNotFound;

        if (answer.SubmitTime is not null)
            return ExamErrors.QuestionAlreadyAnswered;

        if (utcNow > answer.ExamEndDate)
            return ExamErrors.ExamEnded;

        var lastDate = answer.RequestTime.AddSeconds(answer.QuestionOpenForSeconds);
        if (utcNow > lastDate)
            return ExamErrors.QuestionTimeEnd;

        await _context.ExamAnswers
            .Where(x => x.Id == answer.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.ExamQuestionOptionId, optionId)
                .SetProperty(x => x.SubmitTime, utcNow), cancellationToken);

        return Result.Success();
    }
}