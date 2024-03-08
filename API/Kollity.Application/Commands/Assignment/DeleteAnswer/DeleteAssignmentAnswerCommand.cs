namespace Kollity.Application.Commands.Assignment.DeleteAnswer;

public record DeleteAssignmentAnswerCommand(Guid AssignmentId) : ICommand;