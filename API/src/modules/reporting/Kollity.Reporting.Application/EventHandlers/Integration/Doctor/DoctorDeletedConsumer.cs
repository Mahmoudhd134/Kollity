using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.Doctor;

public class DoctorDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<DoctorDeletedIntegrationEvent>
{
    protected override Task Handle(DoctorDeletedIntegrationEvent integrationEvent)
    {
        return context.Doctors
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteDeleteAsync();
    }
}