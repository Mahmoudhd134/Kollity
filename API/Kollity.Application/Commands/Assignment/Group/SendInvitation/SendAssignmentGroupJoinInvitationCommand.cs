using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.SendInvitation;

<<<<<<< HEAD
public record SendAssignmentGroupJoinInvitationCommand(AssignmentGroupInvitationMapDto InvitationDto) : ICommand;
=======
public record SendAssignmentGroupJoinInvitationCommand(AssignmentGroupInvitationMapDto InvitationDto)
    : ICommandWithEvents;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
