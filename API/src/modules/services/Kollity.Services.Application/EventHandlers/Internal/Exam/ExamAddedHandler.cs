using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Exam;
using Kollity.Services.Contracts.Exam;
using Kollity.Services.Domain.ExamModels;

namespace Kollity.Services.Application.EventHandlers.Internal.Exam;

public static class ExamAddedHandler
{
    public class ToIntegration(IEventBus eventBus) : IEventHandler<ExamAddedEvent>
    {
        public Task Handle(ExamAddedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new ExamAddedIntegrationEvent
            {
                Id = notification.Exam.Id,
                Name = notification.Exam.Name,
                EndDate = notification.Exam.EndDate,
                StartDate = notification.Exam.StartDate,
                CreationDate = notification.Exam.CreationDate,
                RoomId = notification.Exam.RoomId
            }, cancellationToken);
        }
    }
}