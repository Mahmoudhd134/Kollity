using Kollity.Services.Application.Dtos.Room.Message;

namespace Kollity.Services.API.Hubs.Hubs.Room;

public interface IRoomHubClient
{
    Task MessageReceived(RoomChatMessageDto dto);
    Task MessageSentSuccessfully(Guid trackId, RoomChatMessageDto dto);
    Task MessageHasNotBeenSentSuccessfully(Guid trackId, List<Error> errors);
    Task MessageDeleted(Guid messageId);
    Task MessageHasNotBeenDeletedSuccessfully(Guid messageId, List<Error> errors);
    Task MessagesHaveBeenRead(List<Guid> messagesId);
    Task PollOptionChosen(Guid pollId, List<byte> options);
    Task PollOptionUnChosen(Guid pollId, List<byte> options);
}