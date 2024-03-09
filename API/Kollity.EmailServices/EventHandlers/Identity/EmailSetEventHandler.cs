using Kollity.Contracts.Events.Identity;
using Kollity.EmailServices.Emails;
using Exception = System.Exception;

namespace Kollity.EmailServices.EventHandlers.Identity;

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