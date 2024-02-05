namespace Application.Commands.Room.Delete;

public record DeleteRoomCommand(Guid RoomId) : ICommand;