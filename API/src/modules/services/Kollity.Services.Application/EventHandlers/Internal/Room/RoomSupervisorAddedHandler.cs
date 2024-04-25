using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomSupervisorAddedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomSupervisorAddedEvent>
    {
        public Task Handle(RoomSupervisorAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomSupervisorAddedIntegrationEvent
            {
                RoomId = notification.UserRoom.RoomId,
                SupervisorId = notification.UserRoom.UserId
            }, cancellationToken);
        }
    }
}