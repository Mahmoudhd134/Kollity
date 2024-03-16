using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.IntegrationEvents.Assignment;

public record AssignmentCreatedEvent(Domain.AssignmentModels.Assignment Assignment) : IEvent;