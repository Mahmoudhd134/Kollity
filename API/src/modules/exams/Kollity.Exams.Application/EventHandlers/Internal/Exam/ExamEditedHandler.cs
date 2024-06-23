using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamEditedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamEditedEvent>
    {
        public Task Handle(ExamEditedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamEditedIntegrationEvent
            {
                Id = notification.Exam.Id,
                Name = notification.Exam.Name,
                EndDate = notification.Exam.EndDate,
                StartDate = notification.Exam.StartDate,
            }, cancellationToken);
        }
    }
}