using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Queries.Assignment.GetIndividualAnswers;

public record GetAssignmentIndividualAnswersQuery(Guid AssignmentId, IndividualAssignmentAnswersFilters Filters)
    : IQuery<IndividualAssignmentAnswersDto>;