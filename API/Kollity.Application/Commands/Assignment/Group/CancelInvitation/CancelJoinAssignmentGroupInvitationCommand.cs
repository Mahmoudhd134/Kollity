using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.Group.CancelInvitation;

public record CancelJoinAssignmentGroupInvitationCommand(AssignmentGroupInvitationMapDto InvitationDto) : ICommand;