using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.Group.DeclineInvitation;

public record DeclineAssignmentGroupJoinInvitationCommand(Guid GroupId) : ICommand;