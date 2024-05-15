using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Commands.Room.Messages.AddPoll;

public record AddChatPollCommand(Guid RoomId, AddChatPollDto AddChatPollDto) : ICommand<RoomChatMessageDto>;