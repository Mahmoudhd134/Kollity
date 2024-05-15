using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Student;

public record StudentAddedEvent(Domain.StudentModels.Student Student, string Password) : IEvent;