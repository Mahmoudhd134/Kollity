using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Application.Events.Courses;

public record CourseStudentDeAssignedEvent(StudentCourse StudentCourse) : IEvent;