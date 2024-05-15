namespace Kollity.Services.Application.Commands.Room.Messages.Delete;

public record DeleteRoomChatMessageCommand(Guid MessageId) : ICommand;