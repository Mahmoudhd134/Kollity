using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Contracts.Student;

namespace Kollity.Services.Application.EventHandlers.Internal.Student;

public static class StudentDeletedHandler
{
    public class ToIntegrations(IEventBus eventBus) : IEventHandler<StudentDeletedEvent>
    {
        public Task Handle(StudentDeletedEvent notification, CancellationToken cancellationToken)
        {
            return eventBus.PublishAsync(new StudentDeletedIntegrationEvent
            {
                Id = notification.Id
            }, cancellationToken);
        }
    }
}