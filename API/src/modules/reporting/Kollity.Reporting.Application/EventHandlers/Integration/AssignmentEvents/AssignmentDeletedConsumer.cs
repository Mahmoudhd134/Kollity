using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentDeletedIntegrationEvent>
{
    protected override async Task Handle(AssignmentDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Assignments
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
        if (result == 0)
            throw new AssignmentExceptions.AssignmentNotFound(integrationEvent.Id);
    }
}