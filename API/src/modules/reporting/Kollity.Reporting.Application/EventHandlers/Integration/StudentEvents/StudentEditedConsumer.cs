using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Student;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.StudentEvents;

public class StudentEditedConsumer(ReportingDbContext context) : IntegrationEventConsumer<StudentEditedIntegrationEvent>
{
    protected override async Task Handle(StudentEditedIntegrationEvent integrationEvent)
    {
        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == integrationEvent.Id);
        if (student is null)
            return;
        student.FullNameInArabic = integrationEvent.FullName;
        student.UserName = integrationEvent.UserName;
        student.Email = integrationEvent.Email;
        await context.SaveChangesAsync();
    }
}