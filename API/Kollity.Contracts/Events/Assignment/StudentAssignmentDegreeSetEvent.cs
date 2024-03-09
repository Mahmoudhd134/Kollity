using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.Assignment;

public record StudentAssignmentDegreeSetEvent(
    Guid AssignmentId,
    Guid RoomId,
    string AssigmentName,
    UserEmailDto Student,
    byte Degree,
    DateTime DegreeSetOn)
    : IEvent;