using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class UsersJoinRequestDeniedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<UsersJoinRequestDeniedEvent>
    {
        public Task Handle(UsersJoinRequestDeniedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomUsersJoinRequestDeniedIntegrationEvent
            {
                Id = notification.RoomId,
                UserIds = notification.UsersId
            }, cancellationToken);
        }
    }
}