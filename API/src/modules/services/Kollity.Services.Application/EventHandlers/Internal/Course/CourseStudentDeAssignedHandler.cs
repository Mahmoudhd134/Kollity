using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseStudentDeAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseStudentDeAssignedEvent>
    {
        public Task Handle(CourseStudentDeAssignedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new StudentDeAssignedFromCourseIntegrationEvent
            {
                StudentId = notification.StudentCourse.StudentId,
                CourseId = notification.StudentCourse.CourseId
            }, cancellationToken);
        }
    }
}