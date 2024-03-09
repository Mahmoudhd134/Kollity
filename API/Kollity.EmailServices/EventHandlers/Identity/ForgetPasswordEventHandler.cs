﻿using Kollity.Contracts.Events.Identity;
using Kollity.EmailServices.Emails;

namespace Kollity.EmailServices.EventHandlers.Identity;

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