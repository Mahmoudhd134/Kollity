using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetById;

public record GetAssignmentGroupByIdQuery(Guid GroupId) : IQuery<AssignmentGroupDto>;