using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.RoomEvents;

public class RoomEditedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<RoomEditedIntegrationEvent>
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