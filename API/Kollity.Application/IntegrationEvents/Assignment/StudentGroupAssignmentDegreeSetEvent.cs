using Kollity.Application.Abstractions.Events;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.IntegrationEvents.Assignment;

public record StudentGroupAssignmentDegreeSetEvent(AssignmentAnswerDegree AssignmentAnswerDegree) : IEvent;