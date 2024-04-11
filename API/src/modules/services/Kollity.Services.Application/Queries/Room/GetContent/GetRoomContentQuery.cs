using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Queries.Room.GetContent;

public record GetRoomContentQuery(Guid RoomId) : IQuery<List<RoomContentDto>>;