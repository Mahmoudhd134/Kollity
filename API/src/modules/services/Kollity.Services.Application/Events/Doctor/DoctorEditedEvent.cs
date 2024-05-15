using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Doctor;

public record DoctorEditedEvent(Domain.DoctorModels.Doctor Doctor) : IEvent;