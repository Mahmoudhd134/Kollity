using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentFileAddedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentFileAddedEvent>
    {
        public Task Handle(AssignmentFileAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentFileAddedIntegrationEvent
            {
                AssignmentId = notification.AssignmentFile.AssignmentId,
                UploadDate = notification.AssignmentFile.UploadDate,
                FileId = notification.AssignmentFile.Id,
                FileName = notification.AssignmentFile.Name
            }, cancellationToken);
        }
    }
}