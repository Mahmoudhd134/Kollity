using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.RoomEvents;

public class RoomUsersJoinedConsumer(FeedbackDbContext context)
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