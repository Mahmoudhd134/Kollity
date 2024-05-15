using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentDegreeSetConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentDegreeSetIntegrationEvent>
{
    protected override async Task Handle(AssignmentDegreeSetIntegrationEvent integrationEvent)
    {
        var result = await context.AssignmentAnswers
            .Where(x => x.AssignmentId == integrationEvent.AssignmentId && x.StudentId == integrationEvent.StudentId)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.Degree, integrationEvent.Degree));
        if (result == 0)
            throw new AssignmentExceptions.AnswerNotFound(integrationEvent.AssignmentId, [integrationEvent.StudentId]);
    }
}