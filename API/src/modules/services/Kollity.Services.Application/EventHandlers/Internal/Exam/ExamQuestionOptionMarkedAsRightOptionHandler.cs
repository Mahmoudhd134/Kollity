using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionOptionMarkedAsRightOptionHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionOptionMarkedAsRightOptionEvent>
    {
        public Task Handle(ExamQuestionOptionMarkedAsRightOptionEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamOptionMarkedAsRightOptionIntegrationEvent
            {
                Id = notification.ExamQuestionOption.Id,
                ExamQuestionId = notification.ExamQuestionOption.ExamQuestionId
            }, cancellationToken);
        }
    }
}