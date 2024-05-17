using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomContentDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<RoomContentDeletedIntegrationEvent>
{
    protected override async Task Handle(RoomContentDeletedIntegrationEvent integrationEvent)
    {
        var content = await context.RoomContents
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.ContentId);
        if (content is null)
            throw new RoomExceptions.RoomContentNotFound(integrationEvent.ContentId);

        context.RoomContents.Remove(content);
        await context.SaveChangesAsync();
    }
}