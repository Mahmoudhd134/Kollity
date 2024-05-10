using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseDeletedEvent>
    {
        public Task Handle(CourseDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new CourseDeletedIntegrationEvent
            {
                Id = notification.Course.Id
            }, cancellationToken);
        }
    }
}