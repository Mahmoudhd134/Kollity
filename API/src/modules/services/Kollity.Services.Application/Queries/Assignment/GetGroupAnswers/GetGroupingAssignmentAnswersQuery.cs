using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;
using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Queries.Assignment.GetGroupAnswers;

public record GetGroupingAssignmentAnswersQuery(Guid AssignmentId, GroupAssignmentAnswersFilters Filters)
    : IQuery<GroupingAssignmentAnswersDto>;