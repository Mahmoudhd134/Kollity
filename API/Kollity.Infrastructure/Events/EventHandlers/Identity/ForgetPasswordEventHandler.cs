using Kollity.Application.IntegrationEvents.Identity;
using Kollity.Infrastructure.Abstraction;

namespace Kollity.Infrastructure.Events.EventHandlers.Identity;

public class ForgetPasswordEventHandler : IEventHandler<ForgetPasswordEvent>
{
    private readonly IEmailService _emailService;

    public ForgetPasswordEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public Task Handle(ForgetPasswordEvent notification, CancellationToken cancellationToken)
    {
        return _emailService.TrySendResetPasswordEmailAsync(notification.Email, notification.Token);
    }
}