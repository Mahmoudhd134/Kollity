using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomDeletedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomDeletedEvent>
    {
        public Task Handle(RoomDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomDeletedIntegrationEvent
            {
                Id = notification.Room.Id
            }, cancellationToken);
        }
    }
}