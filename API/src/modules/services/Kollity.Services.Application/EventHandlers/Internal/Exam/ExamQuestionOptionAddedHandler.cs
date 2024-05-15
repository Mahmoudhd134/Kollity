using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

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