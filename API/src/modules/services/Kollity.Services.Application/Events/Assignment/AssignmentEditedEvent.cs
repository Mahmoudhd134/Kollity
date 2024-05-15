using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Assignment;

public record AssignmentEditedEvent(Domain.AssignmentModels.Assignment Assignment) : IEvent;