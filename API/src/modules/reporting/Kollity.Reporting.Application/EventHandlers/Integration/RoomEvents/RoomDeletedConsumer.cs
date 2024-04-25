using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomDeletedConsumer(ReportingDbContext context) : IntegrationEventConsumer<RoomDeletedIntegrationEvent>
{
    protected override Task Handle(RoomDeletedIntegrationEvent integrationEvent)
    {
        return context.Rooms
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
    }
}