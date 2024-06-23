using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.Events.ExamEvents;
using Kollity.Exams.Contracts.Exam;

namespace Kollity.Exams.Application.EventHandlers.Internal.Exam;

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