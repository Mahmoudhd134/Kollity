using Kollity.User.Contracts.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.EventHandlers.Integration.User;

public class UserEmailEditedConsumer(ApplicationDbContext dbContext) : IConsumer<UserEmailEditedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserEmailEditedIntegrationEvent> context)
    {
        string email = context.Message.Email,
            ue = context.Message.Email?.ToUpper();

        await dbContext.Users
            .Where(x => x.Id == context.Message.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.Email, email)
                .SetProperty(x => x.NormalizedEmail, ue));
    }
}