using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Room;

namespace Kollity.Reporting.Application.EventHandlers.Integration.RoomEvents;

public class RoomUsersJoinedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<RoomUsersJoinedIntegrationEvent>
{
    protected override Task Handle(RoomUsersJoinedIntegrationEvent integrationEvent)
    {
        var roomUsers = integrationEvent.UserIds
            .Select(x => new RoomUser
            {
                RoomId = integrationEvent.Id,
                UserId = x
            });

        context.RoomUsers.AddRange(roomUsers);
        return context.SaveChangesAsync();
    }
}