using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomContentAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<RoomContentAddedIntegrationEvent>
{
    protected override async Task Handle(RoomContentAddedIntegrationEvent integrationEvent)
    {
        var roomExists = await context.Rooms
            .AnyAsync(x => x.Id == integrationEvent.RoomId);
        if (roomExists == false)
            throw new RoomExceptions.RoomNotFound(integrationEvent.RoomId);

        var userExists = await context.Users
            .AnyAsync(x => x.Id == integrationEvent.UploaderId);
        if (userExists == false)
            throw new UserExceptions.UserNotFound(integrationEvent.UploaderId);

        var content = new RoomContent
        {
            Id = integrationEvent.Id,
            RoomId = integrationEvent.RoomId,
            UploaderId = integrationEvent.UploaderId,
            Name = integrationEvent.Name,
            UploadTime = integrationEvent.UploadTime
        };

        context.RoomContents.Add(content);
        await context.SaveChangesAsync();
    }
}