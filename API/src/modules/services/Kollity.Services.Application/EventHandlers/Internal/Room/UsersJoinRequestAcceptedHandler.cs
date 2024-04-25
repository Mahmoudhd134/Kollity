using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class UsersJoinRequestAcceptedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<UsersJoinRequestAcceptedEvent>
    {
        public Task Handle(UsersJoinRequestAcceptedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomUsersJoinedIntegrationEvent
            {
                Id = notification.RoomId,
                UserIds = notification.UsersId
            }, cancellationToken);
        }
    }
}