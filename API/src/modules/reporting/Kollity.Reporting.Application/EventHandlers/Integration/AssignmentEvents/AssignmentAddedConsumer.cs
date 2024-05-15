using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Reporting.Application.EventHandlers.Integration.AssignmentEvents;

public class AssignmentAddedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<AssignmentAddedIntegrationEvent>
{
    protected override Task Handle(AssignmentAddedIntegrationEvent integrationEvent)
    {
        var assignment = new Assignment
        {
            Id = integrationEvent.Id,
            RoomId = integrationEvent.RoomId,
            Degree = integrationEvent.Degree,
            Description = integrationEvent.Description,
            Name = integrationEvent.Name,
            DoctorId = integrationEvent.DoctorId,
            Mode = integrationEvent.Type == AssignmentType.Group ? ReportingAssignmentMode.Group : ReportingAssignmentMode.Individual,
            CreatedDate = integrationEvent.CreatedDate,
            IsDeleted = false,
            LastUpdateDate = integrationEvent.CreatedDate,
            OpenUntilDate = integrationEvent.OpenUntilDate,
        };
        context.Assignments.Add(assignment);
        return context.SaveChangesAsync();
    }
}