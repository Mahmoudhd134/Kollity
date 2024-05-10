using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class StudentGroupAssignmentDegreeSetHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<StudentGroupAssignmentDegreeSetEvent>
    {
        public Task Handle(StudentGroupAssignmentDegreeSetEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new AssignmentDegreeSetIntegrationEvent
            {
                AssignmentId = notification.AssignmentAnswerDegree.AssignmentId,
                StudentId = notification.AssignmentAnswerDegree.StudentId,
                Degree = notification.AssignmentAnswerDegree.Degree
            }, cancellationToken);
        }
    }
}