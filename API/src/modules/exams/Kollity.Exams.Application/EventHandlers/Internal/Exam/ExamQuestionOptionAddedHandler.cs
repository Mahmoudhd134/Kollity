using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionOptionAddedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionOptionAddedEvent>
    {
        public Task Handle(ExamQuestionOptionAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamOptionAddedIntegrationEvent
            {
                Id = notification.ExamQuestionOption.Id,
                Option = notification.ExamQuestionOption.Option,
                ExamQuestionId = notification.ExamQuestionOption.ExamQuestionId,
                IsRightOption = notification.ExamQuestionOption.IsRightOption
            }, cancellationToken);
        }
    }
}