using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Queries.GetDegrees;

public record GetExamDegreesQuery(Guid ExamId, ExamDegreesFilters Filters) : IQuery<ExamDegreesDto>;