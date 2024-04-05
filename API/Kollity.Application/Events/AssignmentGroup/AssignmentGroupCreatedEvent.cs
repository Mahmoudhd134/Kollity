using Kollity.Application.Abstractions.Events;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Events.AssignmentGroup;

public record AssignmentGroupCreatedEvent(AssignmentGroupDto AssignmentGroupDto) : IEvent;