using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.RoomEvents;

public class RoomDeletedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<RoomDeletedIntegrationEvent>
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