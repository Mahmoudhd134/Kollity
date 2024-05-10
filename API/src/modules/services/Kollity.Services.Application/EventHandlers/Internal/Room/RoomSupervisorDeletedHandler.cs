using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomSupervisorDeletedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomSupervisorDeletedEvent>
    {
        public Task Handle(RoomSupervisorDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new RoomSupervisorDeletedIntegrationEvent
            {
                RoomId = notification.UserRoom.RoomId,
                SupervisorId = notification.UserRoom.UserId
            }, cancellationToken);
        }
    }
}