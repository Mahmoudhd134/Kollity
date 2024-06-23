using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamAddedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamAddedEvent>
    {
        public Task Handle(ExamAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamAddedIntegrationEvent
            {
                Id = notification.Exam.Id,
                Name = notification.Exam.Name,
                EndDate = notification.Exam.EndDate,
                StartDate = notification.Exam.StartDate,
                CreationDate = notification.Exam.CreationDate,
                RoomId = notification.Exam.RoomId
            }, cancellationToken);
        }
    }
}