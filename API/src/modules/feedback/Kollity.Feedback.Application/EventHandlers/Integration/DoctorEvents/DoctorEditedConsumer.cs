using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Application.Exceptions;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Feedback.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorEditedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<DoctorEditedIntegrationEvent>
{
    protected override async Task Handle(DoctorEditedIntegrationEvent integrationEvent)
    {
        var doctor = await context.Users
            .FirstOrDefaultAsync(s => s.Id == integrationEvent.Id);
        if (doctor is null)
            throw new UserExceptions.DoctorNotFound(integrationEvent.Id);
        doctor.FullName = integrationEvent.FullName;
        doctor.UserName = integrationEvent.UserName;
        await context.SaveChangesAsync();
    }
}