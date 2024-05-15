using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseEditedHandler
{
    public class ToIntegration(IEventBus eventBus, IMapper mapper) : IEventHandler<CourseEditedEvent>
    {
        public Task Handle(CourseEditedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(mapper.Map<CourseEditedIntegrationEvent>(notification.Course),
                cancellationToken);
        }
    }
}