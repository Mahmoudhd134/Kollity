using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionAddedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionAddedEvent>
    {
        public Task Handle(ExamQuestionAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamQuestionAddedIntegrationEvent
            {
                Id = notification.ExamQuestion.Id,
                Degree = notification.ExamQuestion.Degree,
                Question = notification.ExamQuestion.Question,
                ExamId = notification.ExamQuestion.ExamId,
                OpenForSeconds = notification.ExamQuestion.OpenForSeconds,
                DefaultOptionId = notification.ExamQuestion.ExamQuestionOptions.First().Id
            }, cancellationToken);
        }
    }
}