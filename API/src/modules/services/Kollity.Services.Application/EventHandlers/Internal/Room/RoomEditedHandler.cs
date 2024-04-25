using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomEditedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomEditedEvent>
    {
        public Task Handle(RoomEditedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomEditedIntegrationEvent
            {
                Id = notification.Room.Id,
                Name = notification.Room.Name,
                EnsureJoinRequest = notification.Room.EnsureJoinRequest
            }, cancellationToken);
        }
    }
}