using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Queries.Room.GetContent;

public record GetRoomContentQuery(Guid RoomId) : IQuery<List<RoomContentDto>>;