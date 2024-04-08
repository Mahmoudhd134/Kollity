using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Assignment;

public record AssignmentCreatedEvent(Domain.AssignmentModels.Assignment Assignment) : IEvent;