using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentEditedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentEditedEvent>
    {
        public Task Handle(AssignmentEditedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentEditedIntegrationEvent
            {
                Id = notification.Assignment.Id,
                Type = notification.Assignment.Mode == AssignmentMode.Group
                    ? AssignmentType.Group
                    : AssignmentType.Individual,
                Degree = notification.Assignment.Degree,
                Description = notification.Assignment.Description,
                Name = notification.Assignment.Name,
                OpenUntilDate = notification.Assignment.OpenUntilDate
            }, cancellationToken);
        }
    }
}