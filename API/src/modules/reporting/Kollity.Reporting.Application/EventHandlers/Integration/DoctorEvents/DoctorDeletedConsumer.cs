using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Application.Exceptions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorDeletedConsumer(ReportingDbContext context)
    : IntegrationEventConsumer<DoctorDeletedIntegrationEvent>
{
    protected override async Task Handle(DoctorDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Doctors
            .Where(x => x.Id == integrationEvent.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));

        if (result == 0)
            throw new UserExceptions.DoctorNotFound(integrationEvent.Id);
    }
}