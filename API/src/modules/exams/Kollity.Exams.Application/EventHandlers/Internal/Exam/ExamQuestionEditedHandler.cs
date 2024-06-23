using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionEditedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionEditedEvent>
    {
        public Task Handle(ExamQuestionEditedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamQuestionEditedIntegrationEvent
            {
                Id = notification.ExamQuestion.Id,
                Degree = notification.ExamQuestion.Degree,
                Question = notification.ExamQuestion.Question,
                OpenForSeconds = notification.ExamQuestion.OpenForSeconds
            }, cancellationToken);
        }
    }
}