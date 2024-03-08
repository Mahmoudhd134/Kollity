using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Queries.Assignment.Group.GetById;

public record GetAssignmentGroupByIdQuery(Guid GroupId) : IQuery<AssignmentGroupDto>;