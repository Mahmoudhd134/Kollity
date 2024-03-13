using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Queries.Exam.GetDegrees;

public record GetExamDegreesQuery(Guid ExamId, ExamDegreesFilters Filters) : IQuery<ExamDegreesDto>;