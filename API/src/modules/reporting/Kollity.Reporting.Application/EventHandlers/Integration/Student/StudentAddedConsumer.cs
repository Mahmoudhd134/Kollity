using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Student;

namespace Kollity.Reporting.Application.EventHandlers.Integration.Student;

public class StudentAddedConsumer(ReportingDbContext context) : IntegrationEventConsumer<StudentAddedIntegrationEvent>
{
    protected override Task Handle(StudentAddedIntegrationEvent integrationEvent)
    {
        var student = new Domain.UserModels.Student
        {
            Id = integrationEvent.Id,
            Email = integrationEvent.Email,
            UserName = integrationEvent.UserName,
            Code = integrationEvent.Code,
            FullNameInArabic = integrationEvent.FullName,
            IsDeleted = false,
        };
        context.Students.Add(student);
        return context.SaveChangesAsync();
    }
}