using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Domain.RoomModels;
using Kollity.Services.Contracts.Room;

namespace Kollity.Exams.Application.EventHandlers.Integration.RoomEvents;

public class RoomAddedConsumer(ExamsDbContext context) : IntegrationEventConsumer<RoomAddedIntegrationEvent>
{
    protected override Task Handle(RoomAddedIntegrationEvent integrationEvent)
    {
        var room = new Room
        {
            Id = integrationEvent.Id,
            Name = integrationEvent.Name,
            DoctorId = integrationEvent.DoctorId,
            IsDeleted = false,
        };

        context.Rooms.Add(room);
        return context.SaveChangesAsync();
    }
}