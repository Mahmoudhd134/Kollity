using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class StudentIndividualAssignmentDegreeSetHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<StudentIndividualAssignmentDegreeSetEvent>
    {
        public Task Handle(StudentIndividualAssignmentDegreeSetEvent notification, CancellationToken cancellationToken)
        {
            if (notification.AssignmentAnswer.StudentId is null)
                throw new ArgumentNullException(nameof(notification.AssignmentAnswer.StudentId));
            if (notification.AssignmentAnswer.Degree is null)
                throw new ArgumentNullException(nameof(notification.AssignmentAnswer.Degree));

            return eventBus.PublishAsync(new AssignmentDegreeSetIntegrationEvent
            {
                AssignmentId = notification.AssignmentAnswer.AssignmentId,
                StudentId = notification.AssignmentAnswer.StudentId.Value,
                Degree = notification.AssignmentAnswer.Degree.Value
            }, cancellationToken);
        }
    }
}