using Kollity.Common.Abstractions.Messages;
using Kollity.Common.ErrorHandling;
using Kollity.User.API.Events.Identity;
using Kollity.User.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Mediator.Identity.ResetPassword.SendToken;

public class SendResetPasswordTokenToEmailCommandHandler : ICommandHandler<SendResetPasswordTokenToEmailCommand>
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly IPublisher _publisher;

    public SendResetPasswordTokenToEmailCommandHandler(UserManager<BaseUser> userManager,
        IPublisher publisher)
    {
        _userManager = userManager;
        _publisher = publisher;
    }

    public async Task<Result> Handle(SendResetPasswordTokenToEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return UserErrors.EmailNotFound(request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _publisher.Publish(new ForgetPasswordEvent(user.Email!, token), cancellationToken);

        return Result.Success();
    }
}