namespace Kollity.Services.Application.Commands.Room.Messages.UnPin;

public record UnPinRoomChatMessageCommand(Guid RoomId, Guid MessageId) : ICommand;