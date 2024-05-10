using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

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