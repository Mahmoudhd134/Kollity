using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentFileDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentFileDeletedEvent>
    {
        public Task Handle(AssignmentFileDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentFileDeletedIntegrationEvent
            {
                AssignmentId = notification.AssignmentFile.AssignmentId,
                FileId = notification.AssignmentFile.Id
            }, cancellationToken);
        }
    }
}