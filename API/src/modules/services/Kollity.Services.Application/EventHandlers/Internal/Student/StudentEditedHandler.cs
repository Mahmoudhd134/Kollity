using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Contracts.Student;

namespace Kollity.Services.Application.EventHandlers.Internal.Student;

public static class StudentEditedHandler
{
    public class ToIntegration : IEventHandler<StudentEditedEvent>
    {
        private readonly IEventBus _eventBus;

        public ToIntegration(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(StudentEditedEvent notification, CancellationToken cancellationToken)
        {
            return _eventBus.PublishAsync(new StudentEditedIntegrationEvent
            {
                UserName = notification.Student.UserName,
                Email = notification.Student.Email,
                FullName = notification.Student.FullNameInArabic,
            }, cancellationToken);
        }
    }
}