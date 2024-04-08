using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Commands.Assignment.Group.CancelInvitation;

public record CancelJoinAssignmentGroupInvitationCommand(AssignmentGroupInvitationMapDto InvitationDto) : ICommand;