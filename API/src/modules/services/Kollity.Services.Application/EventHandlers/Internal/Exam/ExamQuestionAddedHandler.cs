using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

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