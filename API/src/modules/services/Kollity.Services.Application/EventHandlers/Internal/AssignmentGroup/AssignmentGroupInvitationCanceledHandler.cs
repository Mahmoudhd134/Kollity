using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupInvitationCanceledHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupInvitationCanceledEvent>
    {
        public Task Handle(AssignmentGroupInvitationCanceledEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupInvitationCanceledIntegrationEvent
            {
                GroupId = notification.AssignmentGroupStudent.Id,
                StudentId = notification.AssignmentGroupStudent.StudentId
            }, cancellationToken);
        }
    }
}