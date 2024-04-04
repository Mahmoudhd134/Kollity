using Kollity.Application.Dtos.Room.Message;
using Kollity.Domain.ErrorHandlers.Abstractions;

namespace Kollity.API.Hubs.Hubs.Room;

public interface IRoomHubClient
{
    Task MessageReceived(RoomChatMessageDto dto);
    Task MessageSentSuccessfully(Guid trackId, RoomChatMessageDto dto);
    Task MessageHasNotBeenSentSuccessfully(Guid trackId, List<Error> errors);
    Task MessageDeleted(Guid messageId);
    Task MessageHasNotBeenDeletedSuccessfully(Guid messageId, List<Error> errors);
    Task MessagesHaveBeenRead(List<Guid> messagesId);
}