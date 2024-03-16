using Kollity.Application.IntegrationEvents.Identity;
using Kollity.Infrastructure.Abstraction;

namespace Kollity.Infrastructure.Events.EventHandlers.Identity;

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