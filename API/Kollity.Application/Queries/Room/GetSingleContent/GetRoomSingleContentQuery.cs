using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Queries.Room.GetSingleContent;

public record GetRoomSingleContentQuery(Guid ContentId) : IQuery<RoomContentFileDto>;