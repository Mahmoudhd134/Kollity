using Kollity.Services.Application.Abstractions.Events;

namespace Kollity.Services.Application.Events.Student;

public record StudentEditedEvent(Domain.StudentModels.Student Student) : IEvent;