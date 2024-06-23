using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamDeletedEvent>
    {
        public Task Handle(ExamDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamDeletedIntegrationEvent
            {
                Id = notification.Exam.Id,
            }, cancellationToken);
        }
    }
}