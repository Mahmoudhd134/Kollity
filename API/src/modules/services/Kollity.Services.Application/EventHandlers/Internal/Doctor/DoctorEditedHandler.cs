using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Contracts.Doctor;

namespace Kollity.Services.Application.EventHandlers.Internal.Doctor;

public static class DoctorEditedHandler
{
    public class ToIntegration : IEventHandler<DoctorEditedEvent>
    {
        private readonly IEventBus _eventBus;

        public ToIntegration(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DoctorEditedEvent notification, CancellationToken cancellationToken)
        {
            return _eventBus.PublishAsync(new DoctorEditedIntegrationEvent
            {
                FullName = notification.Doctor.FullNameInArabic,
                UserName = notification.Doctor.UserName,
                Email = notification.Doctor.Email
            }, cancellationToken);
        }
    }
}