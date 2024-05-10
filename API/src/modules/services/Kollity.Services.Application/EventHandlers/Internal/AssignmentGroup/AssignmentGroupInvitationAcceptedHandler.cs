using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupInvitationAcceptedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupInvitationAcceptedEvent>
    {
        public Task Handle(AssignmentGroupInvitationAcceptedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupInvitationAcceptedIntegrationEvent
            {
                GroupId = notification.AssignmentGroupStudent.Id,
                StudentId = notification.AssignmentGroupStudent.StudentId
            }, cancellationToken);
        }
    }
}