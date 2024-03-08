using Kollity.Application.Dtos.Assignment;

namespace Kollity.Application.Queries.Assignment.GetGroupDegree;

public record GetAssignmentGroupMembersAnswerDegreeQuery(Guid AssignmentId, Guid GroupId)
    : IQuery<AssignmentGroupDegreeDto>;