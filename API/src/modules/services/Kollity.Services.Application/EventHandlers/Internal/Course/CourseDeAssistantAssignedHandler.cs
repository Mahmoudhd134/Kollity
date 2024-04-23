using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseDeAssistantAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseAssistantDeAssignedEvent>
    {
        public Task Handle(CourseAssistantDeAssignedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssistantDeAssignedFromCourseIntegrationEvent
            {
                AssistantId = notification.CourseAssistant.AssistantId,
                CourseId = notification.CourseAssistant.CourseId
            }, cancellationToken);
        }
    }
}