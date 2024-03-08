using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Events.AssignmentGroup.Created;

public record AssignmentGroupCreatedEvent(AssignmentGroupDto Dto, string RoomName, string CourseName) : IEvent;