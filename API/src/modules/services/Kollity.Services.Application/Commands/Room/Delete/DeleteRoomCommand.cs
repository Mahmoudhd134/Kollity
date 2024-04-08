using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.Delete;

public record DeleteRoomCommand(Guid RoomId) : ICommand;