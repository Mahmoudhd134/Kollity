using Kollity.Application.Dtos.Exam;
using Kollity.Domain.ExamModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.GetNextQuestion;

public class GetExamNextQuestionCommandHandler : ICommandHandler<GetExamNextQuestionCommand, ExamQuestionForAnswerDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetExamNextQuestionCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<ExamQuestionForAnswerDto>> Handle(GetExamNextQuestionCommand request,
        CancellationToken cancellationToken)
    {
        Guid examId = request.ExamId,
            userId = _userServices.GetCurrentUserId();

        var utcNow = DateTime.UtcNow;
        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new
            {
                x.StartDate,
                x.EndDate,
                x.RoomId
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);

        if (exam.StartDate > utcNow)
            return ExamErrors.ExamDoseNotStart;

        if (utcNow > exam.EndDate)
            return ExamErrors.ExamEnded;

        var userInRoom = await _context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == exam.RoomId && x.JoinRequestAccepted, cancellationToken);
        if (userInRoom == false)
            return RoomErrors.UserIsNotJoined(userId);

        var allQuestions = await _context.ExamQuestions
            .Where(x => x.ExamId == examId)
            .Select(x => new
            {
                x.Id,
                x.OpenForSeconds,
                Answer = x.ExamAnswers
                    .Where(xx => xx.StudentId == userId)
                    .Select(xx => new
                    {
                        xx.RequestTime,
                        xx.SubmitTime
                    })
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        var unSolvedQuestions = allQuestions
            .Where(x => x.Answer == null ||
                        (x.Answer.SubmitTime is null &&
                         x.Answer.RequestTime.AddSeconds(x.OpenForSeconds) > utcNow))
            .ToList();

        if (unSolvedQuestions.Count == 0)
            return ExamErrors.NoOtherQuestions;

        Guid questionId;
        var indexOfUnAnsweredQuestion = unSolvedQuestions
            .Select((v, i) => new { v, i })
            .FirstOrDefault(x => x.v.Answer != null && x.v.Answer.SubmitTime == null)?.i ?? null;

        if (indexOfUnAnsweredQuestion != null)
        {
            questionId = unSolvedQuestions[(int)indexOfUnAnsweredQuestion].Id;
        }
        else
        {
            var rand = new Random();
            var i = rand.Next(0, unSolvedQuestions.Count);
            questionId = unSolvedQuestions[i].Id;
        }

        var questionDto = await _context.ExamQuestions
            .Where(x => x.Id == questionId)
            .Select(x => new ExamQuestionForAnswerDto()
            {
                Id = x.Id,
                Question = x.Question,
                Degree = x.Degree,
                OpenForSeconds = x.OpenForSeconds,
                Options = x.ExamQuestionOptions.Select(xx => new ExamQuestionOptionForAnswerDto()
                {
                    Id = xx.Id,
                    Option = xx.Option
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);


        if (indexOfUnAnsweredQuestion != null)
        {
            var requestTime = unSolvedQuestions[(int)indexOfUnAnsweredQuestion].Answer.RequestTime;
            var lastTime = requestTime.AddSeconds(questionDto.OpenForSeconds);
            questionDto.OpenForSeconds = (int)(lastTime - utcNow).TotalSeconds;
        }
        else
        {
            var answer = new ExamAnswer()
            {
                ExamId = examId,
                StudentId = userId,
                ExamQuestionId = questionId,
                RequestTime = DateTime.UtcNow
            };
            _context.ExamAnswers.Add(answer);
            await _context.SaveChangesAsync(cancellationToken);
        }

        questionDto.QuestionsCount = allQuestions.Count;
        questionDto.QuestionNumber = allQuestions
            .Count(x => x.Answer != null &&
                        (x.Answer.SubmitTime != null ||
                         utcNow > x.Answer.RequestTime.AddSeconds(x.OpenForSeconds))) + 1;

        return questionDto;
    }
}