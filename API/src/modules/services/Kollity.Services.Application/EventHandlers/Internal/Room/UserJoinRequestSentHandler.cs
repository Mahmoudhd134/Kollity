using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class UserJoinRequestSentHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<UserJoinRequestSentEvent>
    {
        public Task Handle(UserJoinRequestSentEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomUserJoinRequestSentIntegrationEvent
            {
                UserId = notification.UserRoom.UserId,
                RoomId = notification.UserRoom.RoomId
            }, cancellationToken);
        }
    }
}