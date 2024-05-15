namespace Kollity.Services.Application.Commands.Assignment.Group.DeclineInvitation;

public record DeclineAssignmentGroupJoinInvitationCommand(Guid GroupId) : ICommand;