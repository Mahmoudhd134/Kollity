using Kollity.Contracts.Events.Content;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.Room;

public class RoomContentAddedEventHandler : IEventHandler<RoomContentAddedEvent>
{
    private readonly IEmailService _emailService;

    public RoomContentAddedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(RoomContentAddedEvent notification, CancellationToken cancellationToken)
    {
        return _emailService.TrySendRoomContentAddEmailAsync(
            notification.RoomName,
            notification.ContentName,
            notification.AddedAt,
            notification.Students
        );
    }
}