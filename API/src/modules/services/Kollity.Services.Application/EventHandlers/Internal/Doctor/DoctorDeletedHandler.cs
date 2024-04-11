using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Contracts.Doctor;

namespace Kollity.Services.Application.EventHandlers.Internal.Doctor;

public static class DoctorDeletedHandler
{
    public class ToIntegration : IEventHandler<DoctorDeletedEvent>
    {
        private readonly IEventBus _eventBus;

        public ToIntegration(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(DoctorDeletedEvent notification, CancellationToken cancellationToken)
        {
            return _eventBus.PublishAsync(new DoctorDeletedIntegrationEvent
            {
                UserName = notification.Doctor.UserName
            }, cancellationToken);
        }
    }
}