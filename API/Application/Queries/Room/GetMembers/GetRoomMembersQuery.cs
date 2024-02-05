using Application.Dtos.Room;

namespace Application.Queries.Room.GetMembers;

public record GetRoomMembersQuery(Guid RoomId) : IQuery<List<RoomMemberDto>>;