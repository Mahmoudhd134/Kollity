using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.CourseModels;

namespace Kollity.Services.Application.Events.Courses;

public record CourseDoctorDeAssignedEvent(Course Course, Guid DoctorId) : IEvent;