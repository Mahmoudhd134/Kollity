using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentCreatedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentCreatedEvent>
    {
        public Task Handle(AssignmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentAddedIntegrationEvent
            {
                Id = notification.Assignment.Id,
                Type = notification.Assignment.Mode == AssignmentMode.Group
                    ? AssignmentType.Group
                    : AssignmentType.Individual,
                RoomId = notification.Assignment.RoomId,
                Degree = notification.Assignment.Degree,
                Description = notification.Assignment.Description,
                Name = notification.Assignment.Name,
                CreatedDate = notification.Assignment.CreatedDate,
                DoctorId = notification.Assignment.DoctorId ?? Guid.Empty,
                OpenUntilDate = notification.Assignment.OpenUntilDate
            }, cancellationToken);
        }
    }
}