using Kollity.Application.Abstractions.Events;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;

namespace Kollity.Application.IntegrationEvents.AssignmentGroup;

public record AssignmentGroupInvitationSentEvent(AssignmentGroupStudent AssignmentGroupStudent) : IEvent;