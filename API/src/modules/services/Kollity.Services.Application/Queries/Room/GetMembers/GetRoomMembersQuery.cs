using Kollity.Services.Application.Dtos;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Queries.Room.GetMembers;

public record GetRoomMembersQuery(Guid RoomId,RoomMembersFilterDto Dto) : IQuery<List<RoomMemberDto>>;