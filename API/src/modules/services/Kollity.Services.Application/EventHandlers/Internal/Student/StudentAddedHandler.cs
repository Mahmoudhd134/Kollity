using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Contracts.Student;

namespace Kollity.Services.Application.EventHandlers.Internal.Student;

public static class StudentAddedHandler
{
    public class ToIntegration : IEventHandler<StudentAddedEvent>
    {
        private readonly IEventBus _eventBus;

        public ToIntegration(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(StudentAddedEvent notification, CancellationToken cancellationToken)
        {
            return _eventBus.PublishAsync(new StudentAddedIntegrationEvent
            {
                Email = notification.Student.Email,
                UserName = notification.Student.UserName,
                FullName = notification.Student.FullNameInArabic,
                Password = notification.Password,
                Code = notification.Student.Code
            }, cancellationToken);
        }
    }
}