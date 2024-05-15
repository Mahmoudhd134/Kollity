using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Queries.Room.GetMembers;

public record GetRoomMembersQuery(Guid RoomId,RoomMembersFilterDto Dto) : IQuery<List<RoomMemberDto>>;