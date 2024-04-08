using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.Group.AcceptInvitation;

public record AcceptAssignmentGroupInvitationCommand(Guid GroupId) : ICommand;