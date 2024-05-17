using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

public static class ExamQuestionOptionDeletedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamQuestionOptionDeletedEvent>
    {
        public Task Handle(ExamQuestionOptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamOptionDeletedIntegrationEvent
            {
                Id = notification.ExamQuestionOption.Id
            }, cancellationToken);
        }
    }
}