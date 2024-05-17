using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

public static class ExamAnswerSubmittedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamAnswerSubmittedEvent>
    {
        public Task Handle(ExamAnswerSubmittedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.ExamAnswer.ExamQuestionOptionId is null)
                throw new ArgumentNullException(nameof(notification.ExamAnswer.ExamQuestionOptionId));
            if (notification.ExamAnswer.StudentId is null)
                throw new ArgumentNullException(nameof(notification.ExamAnswer.StudentId));
            if (notification.ExamAnswer.SubmitTime is null)
                throw new ArgumentNullException(nameof(notification.ExamAnswer.SubmitTime));

            return eventBus.PublishAsync(new ExamAnswerSubmittedIntegrationEvent
            {
                ExamId = notification.ExamAnswer.ExamId,
                OptionId = notification.ExamAnswer.ExamQuestionOptionId.Value,
                QuestionId = notification.ExamAnswer.ExamQuestionId,
                UserId = notification.ExamAnswer.StudentId.Value,
                RequestTime = notification.ExamAnswer.RequestTime,
                SubmitTime = notification.ExamAnswer.SubmitTime.Value
            }, cancellationToken);
        }
    }
}