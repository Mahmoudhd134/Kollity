using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.Delete;

public record DeleteAssignmentCommand(Guid Id) : ICommand;