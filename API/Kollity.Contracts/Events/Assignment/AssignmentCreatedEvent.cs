using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.Assignment;

public record AssignmentCreatedEvent(AssignmentCreatedEventDto EventDto) : IEvent;