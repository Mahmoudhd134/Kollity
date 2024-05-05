using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomUsersJoinedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<RoomUsersJoinedIntegrationEvent>
{
    protected override async Task Handle(RoomUsersJoinedIntegrationEvent integrationEvent)
    {
        var roomExists = await context.Rooms
            .AnyAsync(x => x.Id == integrationEvent.Id);
        if (roomExists == false)
            throw new RoomExceptions.RoomNotFound(integrationEvent.Id);

        var roomUsers = integrationEvent.UserIds
            .Select(x => new RoomUser
            {
                RoomId = integrationEvent.Id,
                UserId = x
            });

        context.RoomUsers.AddRange(roomUsers);
        await context.SaveChangesAsync();
    }
}