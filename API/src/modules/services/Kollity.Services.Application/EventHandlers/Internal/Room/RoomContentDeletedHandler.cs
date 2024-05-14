using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomContentDeletedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomContentDeletedEvent>
    {
        public Task Handle(RoomContentDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomContentDeletedIntegrationEvent
            {
                RoomId = notification.RoomContent.RoomId,
                ContentId = notification.RoomContent.Id
            }, cancellationToken);
        }
    }
}