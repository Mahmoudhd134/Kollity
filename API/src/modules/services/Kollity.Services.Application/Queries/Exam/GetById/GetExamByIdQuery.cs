using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Queries.Exam.GetById;

public record GetExamByIdQuery(Guid ExamId) : IQuery<ExamDto>;