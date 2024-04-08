using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Events.AssignmentGroup;

public record AssignmentGroupCreatedEvent(AssignmentGroupDto AssignmentGroupDto) : IEvent;