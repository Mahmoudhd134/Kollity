using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Assignment;
using Kollity.Services.Contracts.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.EventHandlers.Internal.Assignment;

public static class AssignmentAnswerAddedHandler
{
    public class ToIntegration(IEventBus eventBus, ApplicationDbContext context)
        : IEventHandler<AssignmentAnswerAddedEvent>
    {
        public async Task Handle(AssignmentAnswerAddedEvent notification, CancellationToken cancellationToken)
        {
            List<Guid> students;
            if (notification.AssignmentAnswer.StudentId is null)
            {
                students = await context.AssignmentGroupStudents
                    .Where(x => x.AssignmentGroupId == notification.AssignmentAnswer.AssignmentGroupId &&
                                x.JoinRequestAccepted)
                    .Select(x => x.StudentId)
                    .ToListAsync(cancellationToken);
            }
            else students = [notification.AssignmentAnswer.StudentId.Value];

            await eventBus.PublishAsync(new AssignmentAnswerSubmittedIntegrationEvent
            {
                AssignmentId = notification.AssignmentAnswer.AssignmentId,
                GroupId = notification.AssignmentAnswer.AssignmentGroupId,
                AnswerId = notification.AssignmentAnswer.Id,
                StudentIds = students
            }, cancellationToken);
        }
    }
}