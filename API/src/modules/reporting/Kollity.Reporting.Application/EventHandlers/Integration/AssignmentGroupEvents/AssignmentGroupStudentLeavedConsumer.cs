using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.AssignmentGroup;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentGroupEvents;

public class AssignmentGroupStudentLeavedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentGroupStudentLeavedIntegrationEvent>
{
    protected override async Task Handle(AssignmentGroupStudentLeavedIntegrationEvent integrationEvent)
    {
        var result = await context.AssignmentGroups
            .Where(x => x.Id == integrationEvent.GroupId && x.StudentId == integrationEvent.StudentId)
            .ExecuteDeleteAsync();
        if (result == 0)
            throw new AssignmentExceptions.GroupNotFound(integrationEvent.GroupId);
    }
}