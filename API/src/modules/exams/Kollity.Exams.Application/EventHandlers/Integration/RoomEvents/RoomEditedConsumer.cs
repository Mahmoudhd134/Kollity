using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.RoomEvents;

public class RoomEditedConsumer(ExamsDbContext context) : IntegrationEventConsumer<RoomEditedIntegrationEvent>
{
    protected override async Task Handle(RoomEditedIntegrationEvent integrationEvent)
    {
        var room = await context.Rooms
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (room is null)
            throw new RoomExceptions.RoomNotFound(integrationEvent.Id);

        room.Name = integrationEvent.Name;

        await context.SaveChangesAsync();
    }
}