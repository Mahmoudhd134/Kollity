using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

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