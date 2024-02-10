using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Queries.Room.GetMembers;

public record GetRoomMembersQuery(Guid RoomId) : IQuery<List<RoomMemberDto>>;