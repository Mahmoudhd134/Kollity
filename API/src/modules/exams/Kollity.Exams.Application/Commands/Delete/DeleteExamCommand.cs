namespace Kollity.Exams.Application.Commands.Delete;

public record DeleteExamCommand(Guid ExamId) : ICommand;