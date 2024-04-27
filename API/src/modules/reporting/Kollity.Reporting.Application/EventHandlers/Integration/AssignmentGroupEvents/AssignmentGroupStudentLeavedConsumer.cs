using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.AssignmentGroup;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentGroupEvents;

public class AssignmentGroupStudentLeavedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentGroupStudentLeavedIntegrationEvent>
{
    protected override Task Handle(AssignmentGroupStudentLeavedIntegrationEvent integrationEvent)
    {
        return context.AssignmentGroups
            .Where(x => x.Id == integrationEvent.GroupId && x.StudentId == integrationEvent.StudentId)
            .ExecuteDeleteAsync();
    }
}