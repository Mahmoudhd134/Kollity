using Kollity.Feedback.Application.Abstractions;
using Kollity.Feedback.Domain;
using Kollity.Feedback.Persistence.Data;
using Kollity.Services.Contracts.Doctor;

namespace Kollity.Feedback.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorAddedConsumer(FeedbackDbContext context) : IntegrationEventConsumer<DoctorAddedIntegrationEvent>
{
    protected override Task Handle(DoctorAddedIntegrationEvent integrationEvent)
    {
        var doctor = new Domain.User
        {
            Id = integrationEvent.Id,
            UserName = integrationEvent.UserName,
            FullName = integrationEvent.FullName,
            IsDeleted = false,
            UserType = UserType.Doctor
        };
        context.Users.Add(doctor);
        return context.SaveChangesAsync();
    }
}