namespace Kollity.Application.Events.AssignmentGroup.InvitationSent;

public record AssignmentGroupInvitationSentEvent(Guid InvitationId) : IEvent;