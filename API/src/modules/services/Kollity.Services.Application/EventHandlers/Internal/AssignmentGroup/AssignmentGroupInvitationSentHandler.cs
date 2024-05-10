using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupInvitationSentHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupInvitationSentEvent>
    {
        public Task Handle(AssignmentGroupInvitationSentEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupInvitationSentIntegrationEvent
            {
                GroupId = notification.AssignmentGroupStudent.Id,
                StudentId = notification.AssignmentGroupStudent.StudentId
            }, cancellationToken);
        }
    }
}