namespace Kollity.Services.Application.Commands.Room.DeleteContent;

public record DeleteRoomContentCommand(Guid RoomId, Guid Id) : ICommand;