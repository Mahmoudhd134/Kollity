namespace Kollity.Application.Events.Assignment.Created;

public record AssignmentCreatedEvent(Guid AssignmentId) : IEvent;