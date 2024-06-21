using Kollity.Reporting.Application.Dtos.Exam;

namespace Kollity.Reporting.Application.Queries.Exam;

public record GetExamStatisticsQuery(Guid Id) : IQuery<ExamStatisticsDto>;