using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.Queries.Assignment.GetIndividualAnswers;

public record GetAssignmentIndividualAnswersQuery(Guid AssignmentId, IndividualAssignmentAnswersFilters Filters)
    : IQuery<IndividualAssignmentAnswersDto>;