using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

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