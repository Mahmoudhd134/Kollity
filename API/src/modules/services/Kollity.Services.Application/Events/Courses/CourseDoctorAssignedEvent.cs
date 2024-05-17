using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.CourseModels;

namespace Kollity.Services.Application.Events.Courses;

public record CourseDoctorAssignedEvent(Course Course) : IEvent;