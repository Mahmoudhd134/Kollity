using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomDeletedConsumer(ReportingDbContext context) : IntegrationEventConsumer<RoomDeletedIntegrationEvent>
{
    protected override async Task Handle(RoomDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Rooms
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));

        if (result == 0)
            throw new RoomExceptions.RoomNotFound(integrationEvent.Id);
    }
}