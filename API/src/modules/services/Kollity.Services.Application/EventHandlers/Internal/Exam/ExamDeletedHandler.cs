using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

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