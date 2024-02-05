using Application.Dtos.Room;

namespace Application.Queries.Room.GetById;

public record GetRoomByIdQuery(Guid Id) : IQuery<RoomDto>;