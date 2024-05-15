using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseStudentAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseStudentAssignedEvent>
    {
        public Task Handle(CourseStudentAssignedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new StudentAssignedToCourseIntegrationEvent
            {
                StudentId = notification.StudentCourse.StudentId,
                CourseId = notification.StudentCourse.CourseId
            }, cancellationToken);
        }
    }
}