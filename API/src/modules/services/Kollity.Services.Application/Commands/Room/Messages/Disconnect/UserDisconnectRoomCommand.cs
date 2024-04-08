using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.Messages.Disconnect;

public record UserDisconnectRoomCommand(Guid RoomId) : ICommand;