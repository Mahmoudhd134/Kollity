using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.User.Contracts.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.UserEvents;

public class UserEmailEditedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<UserEmailEditedIntegrationEvent>
{
    protected override Task Handle(UserEmailEditedIntegrationEvent integrationEvent)
    {
        return context.Users
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.Email, integrationEvent.Email));
    }
}