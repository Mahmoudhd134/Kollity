using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetAll;

public record GetAllAssignmentGroupsForRoomQuery(Guid RoomId) : IQuery<List<AssignmentGroupForListDto>>;