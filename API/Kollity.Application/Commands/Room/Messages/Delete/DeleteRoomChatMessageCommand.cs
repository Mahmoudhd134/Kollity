namespace Kollity.Application.Commands.Room.Messages.Delete;

public record DeleteRoomChatMessageCommand(Guid MessageId) : ICommand;