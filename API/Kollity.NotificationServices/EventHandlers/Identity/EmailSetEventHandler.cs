using Kollity.Contracts.Events.Identity;
using Kollity.NotificationServices.Abstraction;

namespace Kollity.NotificationServices.EventHandlers.Identity;

public class EmailSetEventHandler : IEventHandler<EmailSetEvent>
{
    private readonly IEmailService _emailService;

    public EmailSetEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(EmailSetEvent notification, CancellationToken cancellationToken)
    {
        return _emailService.TrySendConfirmationEmailAsync(notification.Email, notification.Token);
    }
}