using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.User.Contracts.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.UserEvents;

public class UserEmailEditedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<UserEmailEditedIntegrationEvent>
{
    protected override async Task Handle(UserEmailEditedIntegrationEvent integrationEvent)
    {
        var result = await context.Users
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.Email, integrationEvent.Email));
        if (result == 0)
            throw new UserExceptions.UserNotFound(integrationEvent.Id);
    }
}