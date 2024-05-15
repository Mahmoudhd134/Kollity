using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Assignment;

public record StudentIndividualAssignmentDegreeSetEvent(AssignmentAnswer AssignmentAnswer) : IEvent;