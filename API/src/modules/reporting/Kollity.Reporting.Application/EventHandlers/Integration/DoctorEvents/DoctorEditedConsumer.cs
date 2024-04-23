using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorEditedConsumer(ReportingDbContext context) : IntegrationEventConsumer<DoctorEditedIntegrationEvent>
{
    protected override async Task Handle(DoctorEditedIntegrationEvent integrationEvent)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(s => s.Id == integrationEvent.Id);
        if (doctor is null)
            return;
        doctor.FullNameInArabic = integrationEvent.FullName;
        doctor.UserName = integrationEvent.UserName;
        doctor.Email = integrationEvent.Email;
        await context.SaveChangesAsync();
    }
}