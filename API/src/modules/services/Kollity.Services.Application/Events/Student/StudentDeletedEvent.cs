using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Student;

public record StudentDeletedEvent(Guid Id) : IEvent;