using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionDeletedEvent>
    {
        public Task Handle(ExamQuestionDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamQuestionDeletedIntegrationEvent
            {
                Id = notification.ExamQuestion.Id
            }, cancellationToken);
        }
    }
}