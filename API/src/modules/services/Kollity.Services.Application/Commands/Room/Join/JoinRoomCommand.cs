namespace Kollity.Services.Application.Commands.Room.Join;

public record JoinRoomCommand(Guid RoomId) : ICommand;