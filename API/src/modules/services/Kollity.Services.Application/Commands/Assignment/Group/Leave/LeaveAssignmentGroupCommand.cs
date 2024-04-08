using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.Group.Leave;

public record LeaveAssignmentGroupCommand(Guid GroupId) : ICommand;