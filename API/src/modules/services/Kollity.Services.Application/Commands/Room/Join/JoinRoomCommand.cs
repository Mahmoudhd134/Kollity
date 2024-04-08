using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.Join;

public record JoinRoomCommand(Guid RoomId) : ICommand;