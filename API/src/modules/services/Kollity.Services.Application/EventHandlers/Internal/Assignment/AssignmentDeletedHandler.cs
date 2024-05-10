using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentDeletedEvent>
    {
        public Task Handle(AssignmentDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentDeletedIntegrationEvent
            {
                Id = notification.Assignment.Id,
            }, cancellationToken);
        }
    }
}