using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Commands.Room.Messages.GetUnRead;

public record GetUnReadMessagesCommand(Guid RoomId) : ICommand<List<RoomChatMessageDto>>;