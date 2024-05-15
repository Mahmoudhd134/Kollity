using Kollity.User.API.Abstraction.Events;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Events.Identity;

namespace Kollity.User.API.EventHandlers.Internal.Identity;

public static class ForgetPasswordHandler
{
    public class SendEmail(IEmailService emailService) : IEventHandler<ForgetPasswordEvent>
    {
        public Task Handle(ForgetPasswordEvent notification, CancellationToken cancellationToken)
        {
            return emailService.TrySendResetPasswordEmailAsync(notification.Email, notification.Token);
        }
    }
}