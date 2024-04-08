using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Room.Messages.Delete;

public record DeleteRoomChatMessageCommand(Guid MessageId) : ICommand;