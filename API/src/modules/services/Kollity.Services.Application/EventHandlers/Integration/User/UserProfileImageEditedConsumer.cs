using Kollity.User.Contracts.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.EventHandlers.Integration.User;

public class UserProfileImageEditedConsumer(ApplicationDbContext dbContext)
    : IConsumer<UserProfileImageEditedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileImageEditedIntegrationEvent> context)
    {
        await dbContext.Users
            .Where(x => x.Id == context.Message.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.ProfileImage, context.Message.ProfileImage));
    }
}