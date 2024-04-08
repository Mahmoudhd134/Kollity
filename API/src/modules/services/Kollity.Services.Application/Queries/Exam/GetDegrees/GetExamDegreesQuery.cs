using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetDegrees;

public record GetExamDegreesQuery(Guid ExamId, ExamDegreesFilters Filters) : IQuery<ExamDegreesDto>;