namespace Kollity.Services.Application.Commands.Exam.Delete;

public record DeleteExamCommand(Guid ExamId) : ICommand;