using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.DeleteAnswer;

public record DeleteAssignmentAnswerCommand(Guid AssignmentId) : ICommand;