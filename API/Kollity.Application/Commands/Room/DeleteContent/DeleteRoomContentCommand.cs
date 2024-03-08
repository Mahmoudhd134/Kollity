namespace Kollity.Application.Commands.Room.DeleteContent;

public record DeleteRoomContentCommand(Guid RoomId, Guid Id) : ICommand;