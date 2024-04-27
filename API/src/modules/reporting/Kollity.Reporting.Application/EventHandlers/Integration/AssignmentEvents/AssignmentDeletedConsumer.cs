using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentDeletedIntegrationEvent>
{
    protected override Task Handle(AssignmentDeletedIntegrationEvent integrationEvent)
    {
        return context.Assignments
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
    }
}