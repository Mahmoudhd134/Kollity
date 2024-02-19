namespace Kollity.Application.Commands.Assignment.Group.AcceptInvitation;

public record AcceptAssignmentGroupInvitationCommand(Guid GroupId) : ICommand;