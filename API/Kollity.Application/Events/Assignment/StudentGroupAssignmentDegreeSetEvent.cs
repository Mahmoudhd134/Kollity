using Kollity.Application.Abstractions.Events;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Events.Assignment;

public record StudentGroupAssignmentDegreeSetEvent(AssignmentAnswerDegree AssignmentAnswerDegree) : IEvent;