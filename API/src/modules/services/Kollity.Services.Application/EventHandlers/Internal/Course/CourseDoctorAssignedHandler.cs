using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseDoctorAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseDoctorAssignedEvent>
    {
        public Task Handle(CourseDoctorAssignedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Course.DoctorId is null)
                throw new ArgumentNullException(nameof(notification.Course.DoctorId));

            return eventBus.PublishAsync(new DoctorAssignedToCourseIntegrationEvent
            {
                DoctorId = (Guid)notification.Course.DoctorId,
                CourseId = notification.Course.Id
            }, cancellationToken);
        }
    }
}