using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

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