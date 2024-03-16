using Kollity.Application.Abstractions.Events;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.IntegrationEvents.AssignmentGroup;

public record AssignmentGroupCreatedEvent(AssignmentGroupDto AssignmentGroupDto) : IEvent;