using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Exams.Domain;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorDeletedConsumer(ExamsDbContext context)
    : IntegrationEventConsumer<DoctorDeletedIntegrationEvent>
{
    protected override async Task Handle(DoctorDeletedIntegrationEvent integrationEvent)
    {
        var result = await context.Users
            .Where(x => x.Id == integrationEvent.Id && x.UserType == UserType.Doctor)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsDeleted, true));

        if (result == 0)
            throw new UserExceptions.DoctorNotFound(integrationEvent.Id);
    }
}