using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseAddedHandler
{
    public class ToIntegration(IEventBus eventBus, IMapper mapper) : IEventHandler<CourseAddedEvent>
    {
        public Task Handle(CourseAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(mapper.Map<CourseAddedIntegrationEvent>(notification.Course),
                cancellationToken);
        }
    }
}