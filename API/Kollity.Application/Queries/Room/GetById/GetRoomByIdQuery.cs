using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Queries.Room.GetById;

public record GetRoomByIdQuery(Guid Id) : IQuery<RoomDto>;