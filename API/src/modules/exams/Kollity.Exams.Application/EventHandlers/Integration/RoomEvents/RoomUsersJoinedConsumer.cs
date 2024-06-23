using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Exams.Domain.RoomModels;
using Kollity.Services.Contracts.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.RoomEvents;

public class RoomUsersJoinedConsumer(ExamsDbContext context)
    : IntegrationEventConsumer<RoomUsersJoinedIntegrationEvent>
{
    protected override async Task Handle(RoomUsersJoinedIntegrationEvent integrationEvent)
    {
        var roomExists = await context.Rooms
            .AnyAsync(x => x.Id == integrationEvent.Id);
        if (roomExists == false)
            throw new RoomExceptions.RoomNotFound(integrationEvent.Id);

        var usersExists = await context.Users
            .Where(x => integrationEvent.UserIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();
        if (usersExists.Count <= integrationEvent.UserIds.Count)
            throw new UserExceptions.UserNotFound(
                integrationEvent.UserIds.First(x => usersExists.Contains(x) == false));


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