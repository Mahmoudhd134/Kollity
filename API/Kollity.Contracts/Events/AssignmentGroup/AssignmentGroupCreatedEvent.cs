using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.AssignmentGroup;

public record AssignmentGroupCreatedEvent(
    AssignmentGroupForEventDto ForEventDto,
    Guid RoomId,
    string RoomName,
    string CourseName)
    : IEvent;