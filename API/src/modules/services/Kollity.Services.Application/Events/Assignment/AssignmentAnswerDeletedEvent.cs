using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.Events.Assignment;

public record AssignmentAnswerDeletedEvent(AssignmentAnswer AssignmentAnswer) : IEvent;