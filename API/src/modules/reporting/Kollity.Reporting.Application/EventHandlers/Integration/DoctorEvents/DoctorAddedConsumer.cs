using Kollity.Reporting.Application.Abstractions;
using Kollity.Reporting.Persistence.Data;
using Kollity.Services.Contracts.Doctor;
using DoctorType = Kollity.Services.Contracts.Doctor.DoctorType;

namespace Kollity.Reporting.Application.EventHandlers.Integration.DoctorEvents;

public class DoctorAddedConsumer(ReportingDbContext context) : IntegrationEventConsumer<DoctorAddedIntegrationEvent>
{
    protected override Task Handle(DoctorAddedIntegrationEvent integrationEvent)
    {
        var doctor = new Domain.UserModels.Doctor
        {
            Id = integrationEvent.Id,
            Email = integrationEvent.Email,
            UserName = integrationEvent.UserName,
            FullNameInArabic = integrationEvent.FullName,
            IsDeleted = false,
            DoctorType = integrationEvent.Type == DoctorType.Doctor
                ? Domain.UserModels.DoctorType.Doctor
                : Domain.UserModels.DoctorType.Assistant
        };
        context.Doctors.Add(doctor);
        return context.SaveChangesAsync();
    }
}