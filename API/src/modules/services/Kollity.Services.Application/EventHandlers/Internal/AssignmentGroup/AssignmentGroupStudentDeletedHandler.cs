using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Contracts.AssignmentGroup;

namespace Kollity.Services.Application.EventHandlers.Internal.AssignmentGroup;

public static class AssignmentGroupStudentDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<AssignmentGroupStudentDeletedEvent>
    {
        public Task Handle(AssignmentGroupStudentDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentGroupStudentDeletedIntegrationEvent
            {
                GroupId = notification.AssignmentGroupStudent.Id,
                StudentId = notification.AssignmentGroupStudent.StudentId
            }, cancellationToken);
        }
    }
}