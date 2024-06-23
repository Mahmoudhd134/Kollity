using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Queries.GetById;

public record GetExamByIdQuery(Guid ExamId) : IQuery<ExamDto>;