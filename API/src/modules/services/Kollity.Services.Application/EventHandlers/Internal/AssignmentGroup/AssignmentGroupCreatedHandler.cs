using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupCreatedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupCreatedEvent>
    {
        public Task Handle(AssignmentGroupCreatedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupAddedIntegrationEvent
            {
                GroupId = notification.AssignmentGroupDto.Id,
                RoomId = notification.AssignmentGroupDto.RoomId,
                Code = notification.AssignmentGroupDto.Code,
                Students = notification.AssignmentGroupDto.Members.Select(x => (x.Id, x.IsJoined)).ToList()
            }, cancellationToken);
        }
    }
}