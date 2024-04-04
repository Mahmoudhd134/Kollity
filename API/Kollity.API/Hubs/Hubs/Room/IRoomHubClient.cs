using Kollity.Application.Dtos.Room.Message;

namespace Kollity.API.Hubs.Hubs.Room;

public interface IRoomHubClient
{
    void MessageReceived(RoomChatMessageDto dto);
    void MessageSentSuccessfully(Guid trackId, RoomChatMessageDto dto);
}