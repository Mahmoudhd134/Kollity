using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentAnswerSubmittedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentAnswerSubmittedIntegrationEvent>
{
    protected override Task Handle(AssignmentAnswerSubmittedIntegrationEvent integrationEvent)
    {
        var answer = integrationEvent.StudentIds
            .Select(s => new AssignmentAnswer
            {
                AssignmentId = integrationEvent.AssignmentId,
                GroupId = integrationEvent.GroupId,
                StudentId = s,
            });

        context.AssignmentAnswers.AddRange(answer);
        return context.SaveChangesAsync();
    }
}