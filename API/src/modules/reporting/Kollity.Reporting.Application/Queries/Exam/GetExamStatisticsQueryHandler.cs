using Kollity.Common.ErrorHandling;
using Kollity.Reporting.Application.Dtos.Exam;
using Kollity.Reporting.Domain.Errors;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.Queries.Exam;

public class GetExamStatisticsQueryHandler(ReportingDbContext context)
    : IQueryHandler<GetExamStatisticsQuery, ExamStatisticsDto>
{
    public async Task<Result<ExamStatisticsDto>> Handle(GetExamStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var examDto = await context.Exams
            .Where(x => x.Id == request.Id)
            .Select(x => new ExamStatisticsDto
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                CreationDate = x.CreationDate,
                NumberOfAnswers = x.Answers.Select(xx => xx.StudentId).Distinct().Count(),
                MaxDegree = x.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Max(),
                MinDegree = x.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Min(),
                AverageDegree = x.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Average(),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (examDto is null)
            return ExamErrors.NotFound(request.Id);

        examDto.Questions = await context.ExamQuestions
            .Where(x => x.ExamId == request.Id)
            .Select(q => new ExamQuestionForExamStatistics
            {
                Id = q.Id,
                Degree = q.Degree,
                Question = q.Question,
                OpenForSeconds = q.OpenForSeconds,
                Options = q.ExamQuestionOptions
                    .Select(o => new ExamOptionForExamStatistics
                    {
                        Id = o.Id,
                        Option = o.Option,
                        IsRightOption = o.IsRightOption,
                        Count = o.ExamAnswers.Count
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);

        return examDto;
    }
}