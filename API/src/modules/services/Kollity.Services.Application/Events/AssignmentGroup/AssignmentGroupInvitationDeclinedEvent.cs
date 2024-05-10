using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;

namespace Kollity.Services.Application.Events.AssignmentGroup;

public record AssignmentGroupInvitationDeclinedEvent(AssignmentGroupStudent AssignmentGroupStudent) : IEvent;