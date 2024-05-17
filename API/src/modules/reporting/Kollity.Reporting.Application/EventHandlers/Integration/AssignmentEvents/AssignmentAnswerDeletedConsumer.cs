using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentAnswerDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentAnswerDeletedIntegrationEvent>
{
    protected override async Task Handle(AssignmentAnswerDeletedIntegrationEvent integrationEvent)
    {
        var answer = await context.AssignmentAnswers
            .Where(x => x.AssignmentId == integrationEvent.AssignmentId &&
                        integrationEvent.StudentIds.Contains(x.StudentId))
            .ToListAsync();
        if (answer is null || answer.Count == 0)
            throw new AssignmentExceptions.AnswerNotFound(integrationEvent.AssignmentId, integrationEvent.StudentIds);
        
        context.AssignmentAnswers.RemoveRange(answer);
        await context.SaveChangesAsync();
    }
}