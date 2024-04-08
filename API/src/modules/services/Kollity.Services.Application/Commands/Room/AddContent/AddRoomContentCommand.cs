using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Room;

namespace Kollity.Services.Application.Commands.Room.AddContent;

public record AddRoomContentCommand(Guid RoomId, AddRoomContentDto AddRoomContentDto) : ICommand;