using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomEditedConsumer(ReportingDbContext context) : IntegrationEventConsumer<RoomEditedIntegrationEvent>
{
    protected override async Task Handle(RoomEditedIntegrationEvent integrationEvent)
    {
        var room = await context.Rooms
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (room is null)
            return;

        room.Name = integrationEvent.Name;

        await context.SaveChangesAsync();
    }
}