using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Commands.Room.Messages.Add;

public record AddRoomMessageCommand(Guid RoomId, AddRoomMessageDto Dto) : ICommand<RoomChatMessageDto>;