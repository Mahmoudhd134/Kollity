using Kollity.Application.Dtos.Room;

namespace Kollity.Application.Commands.Room.AddContent;

public record AddRoomContentCommand(Guid RoomId,AddRoomContentDto AddRoomContentDto) : ICommand;