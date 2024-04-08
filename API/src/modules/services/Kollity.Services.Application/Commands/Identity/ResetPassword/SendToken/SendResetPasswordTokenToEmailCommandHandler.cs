using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.Identity.User;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Events.Identity;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Services.Application.Commands.Identity.ResetPassword.SendToken;

public class SendResetPasswordTokenToEmailCommandHandler : ICommandHandler<SendResetPasswordTokenToEmailCommand>
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly EventCollection _eventCollection;

    public SendResetPasswordTokenToEmailCommandHandler(UserManager<BaseUser> userManager,
        EventCollection eventCollection)
    {
        _userManager = userManager;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(SendResetPasswordTokenToEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return UserErrors.EmailNotFound(request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        _eventCollection.Raise(new ForgetPasswordEvent(user.Email!, token));
        // var result = await _emailService.TrySendResetPasswordEmailAsync(user.Email, token);

        // return result ? Result.Success() : Error.UnKnown;
        return Result.Success();
    }
}