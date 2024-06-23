using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.RoomEvents;

public class RoomDeletedConsumer(ExamsDbContext context) : IntegrationEventConsumer<RoomDeletedIntegrationEvent>
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