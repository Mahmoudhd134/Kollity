using Kollity.Application.Dtos.Room.Message;

namespace Kollity.Application.Commands.Room.Messages.GetUnRead;

public record GetUnReadMessagesCommand(Guid RoomId) : ICommand<List<RoomChatMessageDto>>;