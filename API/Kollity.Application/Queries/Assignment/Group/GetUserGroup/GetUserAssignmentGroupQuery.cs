using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Queries.Assignment.Group.GetUserGroup;

public record GetUserAssignmentGroupQuery(Guid RoomId) : IQuery<AssignmentGroupDto>;