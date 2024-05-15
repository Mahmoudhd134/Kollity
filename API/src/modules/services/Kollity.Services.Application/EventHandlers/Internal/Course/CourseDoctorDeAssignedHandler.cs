using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseDoctorDeAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseDoctorDeAssignedEvent>
    {
        public Task Handle(CourseDoctorDeAssignedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new DoctorDeAssignedFromCourseIntegrationEvent
            {
                DoctorId = notification.DoctorId,
                CourseId = notification.Course.Id
            }, cancellationToken);
        }
    }
}