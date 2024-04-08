using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.AssignmentGroup;

public record AssignmentGroupInvitationSentEvent(AssignmentGroupStudent AssignmentGroupStudent) : IEvent;