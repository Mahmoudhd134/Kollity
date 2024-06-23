using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Application.Dtos.Exam;
using Kollity.Exams.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Queries.GetReview;

public class GetExamReviewQueryHandler : IQueryHandler<GetExamReviewQuery, ExamForUserReviewDto>
{
    private readonly ExamsDbContext _context;
    private readonly IUserServices _userServices;

    public GetExamReviewQueryHandler(ExamsDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<ExamForUserReviewDto>> Handle(GetExamReviewQuery request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            examId = request.ExamId;

        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new
            {
                x.EndDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
            return ExamErrors.IdNotFound(examId);

        if (DateTime.UtcNow < exam.EndDate)
            return ExamErrors.CanNotReviewBeforeExamEnds;

        var examDto = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new ExamForUserReviewDto
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Name = x.Name,
                Questions = x.ExamQuestions.Select(xx => new ExamQuestionForUserReviewDto
                    {
                        Id = xx.Id,
                        Degree = xx.Degree,
                        OpenForSeconds = xx.OpenForSeconds,
                        Question = xx.Question,
                        Options = xx.ExamQuestionOptions.Select(xxx => new ExamQuestionOptionForUserReviewDto
                            {
                                Id = xxx.Id,
                                Option = xxx.Option,
                                IsRightOption = xxx.IsRightOption
                            })
                            .ToList(),
                        OptionIdChosenByUser = xx.ExamAnswers
                            .Where(xxx => xxx.StudentId == userId)
                            .Select(xxx => xxx.ExamQuestionOptionId)
                            .FirstOrDefault()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        examDto.Degree = examDto.Questions.Sum(x => x.Degree);
        examDto.UserDegree = examDto.Questions
            .Where(x => x.OptionIdChosenByUser is not null)
            .Sum(x => x.Options
                .First(xx => xx.Id == x.OptionIdChosenByUser).IsRightOption
                ? x.Degree
                : 0);


        return examDto;
    }
}