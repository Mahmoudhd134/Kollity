using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionOptionDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionOptionDeletedEvent>
    {
        public Task Handle(ExamQuestionOptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamOptionDeletedIntegrationEvent
            {
                Id = notification.ExamQuestionOption.Id
            }, cancellationToken);
        }
    }
}