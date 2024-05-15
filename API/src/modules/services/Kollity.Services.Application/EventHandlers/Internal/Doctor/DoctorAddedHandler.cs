using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Contracts.Doctor;
using Kollity.Services.Domain.Identity;

namespace Kollity.Services.Application.EventHandlers.Internal.Doctor;

public static class DoctorAddedHandler
{
    public class ToIntegration : IEventHandler<DoctorAddedEvent>
    {
        private readonly IEventBus _eventBus;

        public ToIntegration(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DoctorAddedEvent notification, CancellationToken cancellationToken)
        {
            var type = notification.Role switch
            {
                Role.Doctor => DoctorType.Doctor,
                Role.Assistant => DoctorType.Assistant,
                _ => throw new ArgumentOutOfRangeException(nameof(notification.Role))
            };
            return _eventBus.PublishAsync(new DoctorAddedIntegrationEvent
            {
                Id = notification.Doctor.Id,
                FullName = notification.Doctor.FullNameInArabic,
                UserName = notification.Doctor.UserName,
                Email = notification.Doctor.Email,
                Type = type
            }, cancellationToken);
        }
    }
}