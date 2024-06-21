using Kollity.Common.ErrorHandling;
using Kollity.Feedback.Application.Dtos;
using Kollity.Feedback.Application.Services;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Domain.FeedbackModels;
using Kollity.Feedback.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.Implementation;

public class FeedbackServices(FeedbackDbContext context, IUserServices userServices) : IFeedbackServices
{
    public async Task<Result<List<FeedbackQuestionDto>>> GetAllQuestions(FeedbackCategory category,
        CancellationToken cancellationToken = default)
    {
        return await context.FeedbackQuestions
            .Where(x => x.Category == category)
            .Select(x => new FeedbackQuestionDto
            {
                Id = x.Id,
                Category = x.Category,
                Question = x.Question,
                IsMcq = x.IsMcq
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<Result> AnswerFeedbacks(FeedbacksAnswerDto answers, CancellationToken cancellationToken = default)
    {
        var userId = userServices.GetCurrentUserId();

        var answeredBefore = await context.FeedbackAnswers.AnyAsync(a => a.StudentId == userId && (
            a.CourseId == answers.TargetId ||
            a.DoctorId == answers.TargetId ||
            a.ExamId == answers.TargetId
        ), cancellationToken);
        if (answeredBefore)
            return Error.Conflict("Feedback.AlreadyAnswered", "You already answered this feedback");


        var found = answers.Category switch
        {
            FeedbackCategory.Course => await context.Courses.AnyAsync(x => x.Id == answers.TargetId, cancellationToken),
            FeedbackCategory.Doctor => await context.Users.Where(x => x.UserType == UserType.Doctor)
                .AnyAsync(x => x.Id == answers.TargetId, cancellationToken),
            FeedbackCategory.Exam => await context.Exams.AnyAsync(x => x.Id == answers.TargetId, cancellationToken),
        };


        if (found == false)
            return answers.Category switch
            {
                FeedbackCategory.Course => Error.NotFound("Course.NotFound",
                    $"There are no course with id {answers.TargetId}"),
                FeedbackCategory.Doctor => Error.NotFound("Doctor.NotFound",
                    $"There are no doctor with id {answers.TargetId}"),
                FeedbackCategory.Exam => Error.NotFound("Exam.NotFound",
                    $"There are no exam with id {answers.TargetId}"),
            };

        var questions = await context.FeedbackQuestions
            .Where(x => x.Category == answers.Category)
            .Select(x => new { x.Id, x.IsMcq })
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        if (questions.Count != answers.Answers.Count)
            return Error.Conflict("Feedback.QuestionsNotComplete", "You must answer all questions at once");

        var outQuestions = answers.Answers
            .Where(a => questions.ContainsKey(a.QuestionId) == false)
            .Select(a => Error.NotFound("Feedback.QuestionNotFound", $"question with id {a.QuestionId} not found"))
            .ToList();
        if (outQuestions.Any())
            return outQuestions;


        var errors = answers.Answers.Where(a => (
                ((questions.GetValueOrDefault(a.QuestionId)?.IsMcq ?? false) &&
                 a.Answer != null) ||
                (questions.GetValueOrDefault(a.QuestionId)?.IsMcq ?? true) == false &&
                a.Answer == null && a.StringAnswer?.Length > 0) == false)
            .Select(a =>
            {
                var q = questions.GetValueOrDefault(a.QuestionId);
                return Error.Validation("Feedback.BadFormat", $"question {q.Id} is in bad format");
            })
            .ToList();
        if (errors.Any())
            return errors;

        var feedbackAnswers = answers.Answers
            .Select(x => new FeedbackAnswer()
            {
                StudentId = userId,
                QuestionId = x.QuestionId,
                Answer = x.Answer,
                StringAnswer = x.StringAnswer,
                CourseId = answers.Category == FeedbackCategory.Course ? answers.TargetId : null,
                DoctorId = answers.Category == FeedbackCategory.Doctor ? answers.TargetId : null,
                ExamId = answers.Category == FeedbackCategory.Exam ? answers.TargetId : null
            });

        context.FeedbackAnswers.AddRange(feedbackAnswers);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<List<FeedbackAvailableCategory>>> AvailableFeedbacks(
        CancellationToken cancellationToken = default)
    {
        var userId = userServices.GetCurrentUserId();
        var answered = (await context.FeedbackAnswers
                .Where(x => x.StudentId == userId)
                .Select(x => new
                {
                    x.Question.Category,
                    TargetId = x.CourseId ?? (x.DoctorId ?? x.ExamId)
                })
                .Distinct()
                .ToListAsync(cancellationToken))
            .GroupBy(x => x.Category)
            .Select(x => new
            {
                Category = x.Key,
                Ids = x.Select(a => a.TargetId)
            })
            .ToList();

        var examsAnswered = answered.FirstOrDefault(x => x.Category == FeedbackCategory.Exam)?.Ids ?? [];
        var doctorsAnswered = answered.FirstOrDefault(x => x.Category == FeedbackCategory.Doctor)?.Ids ?? [];
        var coursesAnswered = answered.FirstOrDefault(x => x.Category == FeedbackCategory.Course)?.Ids ?? [];

        var requiredExams = await context.RoomUsers
            .Where(x => x.UserId == userId)
            .SelectMany(r => r.Room.Exams)
            .Where(x => examsAnswered.Contains(x.Id) == false)
            .Select(x => new IdNameMap()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        var requiredDoctors = await context.RoomUsers
            .Where(x => x.UserId == userId)
            .Where(x => doctorsAnswered.Contains(x.Room.DoctorId) == false)
            .Select(x => new IdNameMap()
            {
                Id = x.Room.DoctorId,
                Name = x.Room.Doctor.FullName
            })
            .ToListAsync(cancellationToken);

        var requiredCourses = await context.CourseStudents
            .Where(x => x.StudentId == userId)
            .Where(x => coursesAnswered.Contains(x.CourseId) == false)
            .Select(x => new IdNameMap()
            {
                Id = x.CourseId,
                Name = x.Course.Name
            })
            .ToListAsync(cancellationToken);

        return new List<FeedbackAvailableCategory>()
        {
            new()
            {
                Category = FeedbackCategory.Exam,
                Available = requiredExams
            },
            new()
            {
                Category = FeedbackCategory.Course,
                Available = requiredCourses
            },
            new()
            {
                Category = FeedbackCategory.Doctor,
                Available = requiredDoctors
            },
        };
    }

    public async Task<Result<Guid>> AddQuestion(AddFeedbackQuestionDto dto,
        CancellationToken cancellationToken = default)
    {
        var feedbackQuestion = new FeedbackQuestion()
        {
            IsMcq = dto.IsMcq,
            Category = dto.Category,
            Question = dto.Question
        };
        context.FeedbackQuestions.Add(feedbackQuestion);
        await context.SaveChangesAsync(cancellationToken);
        return feedbackQuestion.Id;
    }

    public async Task<Result> DeleteQuestion(Guid id, CancellationToken cancellationToken = default)
    {
        await context.FeedbackAnswers
            .Where(x => x.QuestionId == id)
            .ExecuteDeleteAsync(cancellationToken);
        var result = await context.FeedbackQuestions
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        if (result == 0)
            return Error.NotFound("Feedback.QuestionNotFound", $"there are no question with id {id}");
        return Result.Success();
    }

    public async Task<Result<FeedbackStatistics>> GetStatistics(Guid targetId, FeedbackCategory category,
        CancellationToken cancellationToken = default)
    {
        var isDoctor = category == FeedbackCategory.Doctor;
        var isCourse = category == FeedbackCategory.Course;
        var isExam = category == FeedbackCategory.Exam;

        var result = await context.FeedbackAnswers
            .Where(x => isDoctor == false || x.DoctorId == targetId)
            .Where(x => isCourse == false || x.CourseId == targetId)
            .Where(x => isExam == false || x.ExamId == targetId)
            .Where(x => x.Answer != null)
            .Select(x => new
            {
                x.QuestionId,
                x.Question.Question,
                x.Answer
            })
            .GroupBy(x => x.QuestionId)
            .Select(g => new FeedbackStatisticsQuestionDto()
            {
                Id = g.Key,
                Question = g.First().Question,
                Options = g.GroupBy(x => x.Answer)
                    .Select(ag => new FeedbackStatisticsQuestionOptionDto()
                    {
                        Answer = ag.Key.Value,
                        Count = ag.Count()
                    }).ToList()
            })
            .ToListAsync(cancellationToken);

        var name = category switch
        {
            FeedbackCategory.Course => await context.Courses
                .Where(x => x.Id == targetId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync(cancellationToken),
            FeedbackCategory.Doctor => await context.Users
                .Where(x => x.Id == targetId)
                .Select(x => x.FullName)
                .FirstOrDefaultAsync(cancellationToken),
            FeedbackCategory.Exam => await context.Exams
                .Where(x => x.Id == targetId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync(cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };


        var total = result.Any() ? result.Max(x => x.Options.Sum(o => o.Count)) : 0;

        var statistics = new FeedbackStatistics()
        {
            TargetId = targetId,
            TargetName = name,
            Category = category,
            Questions = result,
            TotalParticipants = total
        };

        return statistics;
    }

    public async Task<Result<List<FeedbackAnswerDto>>> GetStringAnswersForQuestion(Guid questionId, Guid targetId,
        FeedbackCategory category, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var isDoctor = category == FeedbackCategory.Doctor;
        var isCourse = category == FeedbackCategory.Course;
        var isExam = category == FeedbackCategory.Exam;

        var result = await context.FeedbackAnswers
            .Where(x => isDoctor == false || x.DoctorId == targetId)
            .Where(x => isCourse == false || x.CourseId == targetId)
            .Where(x => isExam == false || x.ExamId == targetId)
            .Where(x => x.Answer == null)
            .Where(x => x.StringAnswer != null)
            .Select(x => new FeedbackAnswerDto()
            {
                QuestionId = x.QuestionId,
                StringAnswer = x.StringAnswer
            })
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return result;
    }
}