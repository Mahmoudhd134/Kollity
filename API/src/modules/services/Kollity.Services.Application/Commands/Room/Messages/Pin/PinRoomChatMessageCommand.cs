using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Commands.Room.Messages.Pin;

public record PinRoomChatMessageCommand(Guid RoomId, Guid MessageId) : ICommand<RoomChatMessageDto>;