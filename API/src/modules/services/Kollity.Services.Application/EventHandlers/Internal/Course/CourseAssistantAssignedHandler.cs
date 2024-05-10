using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Courses;
using Kollity.Services.Contracts.Course;

namespace Kollity.Services.Application.EventHandlers.Internal.Course;

public class CourseAssistantAssignedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<CourseAssistantAssignedEvent>
    {
        public Task Handle(CourseAssistantAssignedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssistantAssignedToCourseIntegrationEvent
            {
                AssistantId = notification.CourseAssistant.AssistantId,
                CourseId = notification.CourseAssistant.CourseId
            }, cancellationToken);
        }
    }
}