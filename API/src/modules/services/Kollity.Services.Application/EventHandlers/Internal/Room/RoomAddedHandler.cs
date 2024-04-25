using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Contracts.Room;
using Kollity.Services.Domain.RoomModels;
using Microsoft.Extensions.Logging;

namespace Kollity.Services.Application.EventHandlers.Internal.Room;

public static class RoomAddedHandler
{
    public class AddTheCreatorAsAdminUser(ApplicationDbContext context) : IEventHandler<RoomAddedEvent>
    {
        public Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Room.DoctorId is null)
                throw new ArgumentNullException(nameof(notification.Room.DoctorId));

            context.UserRooms.Add(new UserRoom
            {
                RoomId = notification.Room.Id,
                UserId = notification.Room.DoctorId.Value,
                JoinRequestAccepted = true,
                LastOnlineDate = DateTime.UtcNow,
                IsSupervisor = true
            });

            return context.SaveChangesAsync(cancellationToken);
        }
    }

    public class ToIntegrations(IEventBus eventBus) : IEventHandler<RoomAddedEvent>
    {
        public Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Room.DoctorId is null)
                throw new ArgumentNullException(nameof(notification.Room.DoctorId));
            return eventBus.PublishAsync(new RoomAddedIntegrationEvent
            {
                Id = notification.Room.Id,
                CourseId = notification.Room.CourseId,
                DoctorId = notification.Room.DoctorId.Value,
                Name = notification.Room.Name,
                EnsureJoinRequest = notification.Room.EnsureJoinRequest
            }, cancellationToken);
        }
    }
}