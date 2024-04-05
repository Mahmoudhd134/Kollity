using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Events.Assignment;

public record AssignmentCreatedEvent(Domain.AssignmentModels.Assignment Assignment) : IEvent;