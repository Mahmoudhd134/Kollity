using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos.Email;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Identity.ResetPassword.SendToken;

public class SendResetPasswordTokenToEmailCommandHandler : ICommandHandler<SendResetPasswordTokenToEmailCommand>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<BaseUser> _userManager;

    public SendResetPasswordTokenToEmailCommandHandler(UserManager<BaseUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result> Handle(SendResetPasswordTokenToEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return UserErrors.EmailNotFound(request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _emailService.TrySendResetPasswordEmailAsync(user.Email, token);

        return result ? Result.Success() : Error.UnKnown;
    }
}