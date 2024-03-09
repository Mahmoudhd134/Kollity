using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.AssignmentGroup;

public record AssignmentGroupInvitationSentEvent(AssignmentGroupInvitationEventDto Dto) : IEvent;