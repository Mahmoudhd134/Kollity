using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Queries.Room.GetById;

public record GetRoomByIdQuery(Guid Id) : IQuery<RoomDto>;