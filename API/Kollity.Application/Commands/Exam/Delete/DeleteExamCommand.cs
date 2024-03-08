namespace Kollity.Application.Commands.Exam.Delete;

public record DeleteExamCommand(Guid ExamId) : ICommand;