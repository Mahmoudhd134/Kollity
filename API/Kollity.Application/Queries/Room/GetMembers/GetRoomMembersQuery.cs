using Kollity.Application.Dtos;
using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Queries.Room.GetMembers;

public record GetRoomMembersQuery(Guid RoomId,RoomMembersFilterDto Dto) : IQuery<List<RoomMemberDto>>;