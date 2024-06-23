using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Exceptions;
using Kollity.Exams.Domain;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorEditedConsumer(ExamsDbContext context) : IntegrationEventConsumer<DoctorEditedIntegrationEvent>
{
    protected override async Task Handle(DoctorEditedIntegrationEvent integrationEvent)
    {
        var doctor = await context.Users
            .Where(x => x.UserType == UserType.Doctor)
            .FirstOrDefaultAsync(s => s.Id == integrationEvent.Id);
        if (doctor is null)
            throw new UserExceptions.DoctorNotFound(integrationEvent.Id);
        doctor.FullName = integrationEvent.FullName;
        doctor.UserName = integrationEvent.UserName;
        await context.SaveChangesAsync();
    }
}