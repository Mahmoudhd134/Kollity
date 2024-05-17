using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomAddedConsumer(ReportingDbContext context) : IntegrationEventConsumer<RoomAddedIntegrationEvent>
{
    protected override Task Handle(RoomAddedIntegrationEvent integrationEvent)
    {
        var room = new Room
        {
            Id = integrationEvent.Id,
            Name = integrationEvent.Name,
            DoctorId = integrationEvent.DoctorId,
            CourseId = integrationEvent.CourseId,
            CreatedAt = integrationEvent.EventPublishedDateOnUtc,
            IsDeleted = false
        };

        context.Rooms.Add(room);
        return context.SaveChangesAsync();
    }
}