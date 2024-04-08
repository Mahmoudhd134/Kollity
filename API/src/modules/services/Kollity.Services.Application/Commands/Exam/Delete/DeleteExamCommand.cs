using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Exam.Delete;

public record DeleteExamCommand(Guid ExamId) : ICommand;