using Kollity.User.API.Abstraction.Events;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Events.Identity;

namespace Kollity.User.API.EventHandlers.Internal.Identity;

public static class EmailSetHandler
{
    public class SendConfirmationEmail : IEventHandler<EmailSetEvent>
    {
        private readonly IEmailService _emailService;

        public SendConfirmationEmail(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Handle(EmailSetEvent notification, CancellationToken cancellationToken)
        {
            return _emailService.TrySendConfirmationEmailAsync(notification.Email, notification.Token);
        }
    }
}