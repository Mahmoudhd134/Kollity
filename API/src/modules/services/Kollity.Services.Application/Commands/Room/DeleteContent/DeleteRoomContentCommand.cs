using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.DeleteContent;

public record DeleteRoomContentCommand(Guid RoomId, Guid Id) : ICommand;