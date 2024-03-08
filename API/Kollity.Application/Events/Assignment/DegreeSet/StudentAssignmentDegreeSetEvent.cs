namespace Kollity.Application.Events.Assignment.DegreeSet;

public record StudentAssignmentDegreeSetEvent(Guid AssignmentId, Guid StudentId, byte Degree, DateTime DegreeSetOn)
    : IEvent;