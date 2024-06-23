using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Domain;
using Kollity.Services.Contracts.Doctor;

namespace Kollity.Exams.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorAddedConsumer(ExamsDbContext context) : IntegrationEventConsumer<DoctorAddedIntegrationEvent>
{
    protected override Task Handle(DoctorAddedIntegrationEvent integrationEvent)
    {
        var doctor = new User
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