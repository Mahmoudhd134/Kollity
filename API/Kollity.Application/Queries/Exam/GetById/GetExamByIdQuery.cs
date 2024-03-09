using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Queries.Exam.GetById;

public record GetExamByIdQuery(Guid ExamId) : IQuery<ExamDto>;