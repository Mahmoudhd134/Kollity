using Kollity.Application.Dtos.Assignment;
using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Queries.Assignment.Group.GetAll;

public record GetAllAssignmentGroupsForRoomQuery(Guid RoomId) : IQuery<List<AssignmentGroupForListDto>>;