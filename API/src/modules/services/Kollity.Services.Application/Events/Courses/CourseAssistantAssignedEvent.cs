using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.CourseModels;

namespace Kollity.Services.Application.Events.Courses;

public record CourseAssistantAssignedEvent(CourseAssistant CourseAssistant) : IEvent;