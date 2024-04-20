using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.Student;

public class StudentDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<StudentDeletedIntegrationEvent>
{
    protected override Task Handle(StudentDeletedIntegrationEvent integrationEvent)
    {
        return context.Students
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteDeleteAsync();
    }
}