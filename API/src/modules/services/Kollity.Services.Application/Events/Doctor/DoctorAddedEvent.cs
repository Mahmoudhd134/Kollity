using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Doctor;

public record DoctorAddedEvent(Domain.DoctorModels.Doctor Doctor, string Password, string Role) : IEvent;