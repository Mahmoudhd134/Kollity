using Kollity.Application.Dtos.Room.Message;

namespace Kollity.Application.Commands.Room.Messages.Add;

public record AddRoomMessageCommand(Guid RoomId, AddRoomMessageDto Dto) : ICommand;