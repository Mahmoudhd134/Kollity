using Kollity.Application.Abstractions.Events;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Events.Assignment;

public record StudentIndividualAssignmentDegreeSetEvent(AssignmentAnswer AssignmentAnswer) : IEvent;