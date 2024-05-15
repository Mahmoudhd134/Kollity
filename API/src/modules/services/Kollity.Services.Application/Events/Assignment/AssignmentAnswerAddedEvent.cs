using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.Events.Assignment;

public record AssignmentAnswerAddedEvent(AssignmentAnswer AssignmentAnswer) : IEvent;