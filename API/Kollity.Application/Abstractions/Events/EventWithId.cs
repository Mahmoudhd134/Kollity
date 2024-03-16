namespace Kollity.Application.Abstractions.Events;

public record EventWithId(IEvent Event, Guid Id);