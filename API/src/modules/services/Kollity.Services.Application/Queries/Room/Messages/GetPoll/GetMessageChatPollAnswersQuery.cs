using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.Application.Queries.Room.Messages.GetPoll;

public record GetMessageChatPollAnswersQuery(Guid PollId) : IQuery<ChatPollDto>;