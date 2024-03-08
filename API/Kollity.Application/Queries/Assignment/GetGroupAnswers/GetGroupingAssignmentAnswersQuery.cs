using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Queries.Assignment.GetGroupAnswers;

public record GetGroupingAssignmentAnswersQuery(Guid AssignmentId, GroupAssignmentAnswersFilters Filters)
    : IQuery<GroupingAssignmentAnswersDto>;