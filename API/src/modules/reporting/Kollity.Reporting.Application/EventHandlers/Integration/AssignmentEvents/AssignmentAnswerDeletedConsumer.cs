using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentAnswerDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentAnswerDeletedIntegrationEvent>
{
    protected override Task Handle(AssignmentAnswerDeletedIntegrationEvent integrationEvent)
    {
        return context.AssignmentAnswers
            .Where(x => x.AssignmentId == integrationEvent.AssignmentId &&
                        integrationEvent.StudentIds.Contains(x.StudentId))
            .ExecuteDeleteAsync();
    }
}