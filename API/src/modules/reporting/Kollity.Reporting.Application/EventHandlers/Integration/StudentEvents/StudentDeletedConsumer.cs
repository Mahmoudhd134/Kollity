using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.StudentEvents;

public class StudentDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<StudentDeletedIntegrationEvent>
{
    protected override async Task Handle(StudentDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Students
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));
        if (result == 0)
            throw new UserExceptions.StudentNotFound(integrationEvent.Id);
    }
}