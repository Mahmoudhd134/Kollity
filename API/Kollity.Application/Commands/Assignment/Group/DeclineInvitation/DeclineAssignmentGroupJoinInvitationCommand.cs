namespace Kollity.Application.Commands.Assignment.Group.DeclineInvitation;

public record DeclineAssignmentGroupJoinInvitationCommand(Guid GroupId) : ICommand;