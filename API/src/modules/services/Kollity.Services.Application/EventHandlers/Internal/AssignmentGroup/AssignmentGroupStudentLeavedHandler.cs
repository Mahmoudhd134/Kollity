using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupStudentLeavedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupStudentLeavedEvent>
    {
        public Task Handle(AssignmentGroupStudentLeavedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupStudentLeavedIntegrationEvent
            {
                GroupId = notification.AssignmentGroupStudent.Id,
                StudentId = notification.AssignmentGroupStudent.StudentId
            }, cancellationToken);
        }
    }
}