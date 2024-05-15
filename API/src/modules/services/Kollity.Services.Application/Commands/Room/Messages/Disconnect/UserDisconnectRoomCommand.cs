namespace Kollity.Services.Application.Commands.Room.Messages.Disconnect;

public record UserDisconnectRoomCommand(Guid RoomId) : ICommand;