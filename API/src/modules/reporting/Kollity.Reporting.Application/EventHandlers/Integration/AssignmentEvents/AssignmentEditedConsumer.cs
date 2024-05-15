using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Application.Exceptions.Generic;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentEditedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentEditedIntegrationEvent>
{
    protected override async Task Handle(AssignmentEditedIntegrationEvent integrationEvent)
    {
        var assignment = await context.Assignments
            .FirstOrDefaultAsync(x => x.Id == integrationEvent.Id);
        if (assignment is null)
            throw new AssignmentExceptions.AssignmentNotFound(integrationEvent.Id);

        assignment.Degree = integrationEvent.Degree;
        assignment.Description = integrationEvent.Description;
        assignment.Name = integrationEvent.Name;
        assignment.Mode = integrationEvent.Type == AssignmentType.Group
            ? ReportingAssignmentMode.Group
            : ReportingAssignmentMode.Individual;
        assignment.LastUpdateDate = integrationEvent.EventPublishedDateOnUtc;
        assignment.OpenUntilDate = integrationEvent.OpenUntilDate;

        await context.SaveChangesAsync();
    }
}