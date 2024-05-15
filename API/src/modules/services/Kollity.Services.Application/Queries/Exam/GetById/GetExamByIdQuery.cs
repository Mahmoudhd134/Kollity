using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetById;

public record GetExamByIdQuery(Guid ExamId) : IQuery<ExamDto>;