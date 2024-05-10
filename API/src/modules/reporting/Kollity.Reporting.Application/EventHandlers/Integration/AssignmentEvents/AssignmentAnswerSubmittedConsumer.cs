using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentAnswerSubmittedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentAnswerSubmittedIntegrationEvent>
{
    protected override async Task Handle(AssignmentAnswerSubmittedIntegrationEvent integrationEvent)
    {
        var roomId = await context.Assignments
            .Where(x => x.Id == integrationEvent.AssignmentId)
            .Select(x => x.RoomId)
            .FirstOrDefaultAsync();

        var answer = integrationEvent.StudentIds
            .Select(s => new AssignmentAnswer
            {
                AssignmentId = integrationEvent.AssignmentId,
                GroupId = integrationEvent.GroupId,
                StudentId = s,
                RoomId = roomId
            });

        context.AssignmentAnswers.AddRange(answer);
        await context.SaveChangesAsync();
    }
}