using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Application.Exceptions.Generic;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.AssignmentGroup;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentGroupEvents;

public class AssignmentGroupInvitationAcceptedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentGroupInvitationAcceptedIntegrationEvent>
{
    protected override async Task Handle(AssignmentGroupInvitationAcceptedIntegrationEvent integrationEvent)
    {
        var group = await context.AssignmentGroups
            .Where(x => x.Id == integrationEvent.GroupId)
            .Select(x => new { x.Code, x.RoomId })
            .FirstOrDefaultAsync();

        if (group is null)
            throw new AssignmentExceptions.GroupNotFound(integrationEvent.GroupId);

        var assignmentGroup = new AssignmentGroup
        {
            Id = integrationEvent.GroupId,
            RoomId = group.RoomId,
            StudentId = integrationEvent.StudentId,
            Code = group.Code
        };

        context.AssignmentGroups.Add(assignmentGroup);
        await context.SaveChangesAsync();
    }
}