using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.SendInvitation;

public record SendAssignmentGroupJoinInvitationCommand(AssignmentGroupInvitationMapDto InvitationDto)
    : ICommandWithEvents;