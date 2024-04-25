using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomContentAddedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomContentAddedEvent>
    {
        public Task Handle(RoomContentAddedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.RoomContent.UploaderId is null)
                throw new ArgumentNullException(nameof(notification.RoomContent.UploaderId));
            return eventBus.PublishAsync(new RoomContentAddedIntegrationEvent
            {
                Id = notification.RoomContent.Id,
                RoomId = notification.RoomContent.RoomId,
                Name = notification.RoomContent.Name,
                UploaderId = notification.RoomContent.UploaderId.Value,
                UploadTime = notification.RoomContent.UploadTime
            }, cancellationToken);
        }
    }
}