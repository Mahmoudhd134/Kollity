using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentGroupEvents;

public class AssignmentGroupAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentGroupAddedIntegrationEvent>
{
    protected override Task Handle(AssignmentGroupAddedIntegrationEvent integrationEvent)
    {
        var assignmentGroup = integrationEvent.Students
            .Where(x => x.isJoined)
            .Select(x => new AssignmentGroup
            {
                Id = integrationEvent.GroupId,
                Code = integrationEvent.Code,
                RoomId = integrationEvent.RoomId,
                StudentId = x.id
            });

        context.AssignmentGroups.AddRange(assignmentGroup);
        return context.SaveChangesAsync();
    }
}