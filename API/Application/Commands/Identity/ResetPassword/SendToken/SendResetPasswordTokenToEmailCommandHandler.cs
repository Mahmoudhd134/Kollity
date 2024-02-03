using Application.Abstractions;
using Application.Dtos.Email;
using Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Identity.ResetPassword.SendToken;

public class SendResetPasswordTokenToEmailCommandHandler : ICommandHandler<SendResetPasswordTokenToEmailCommand>
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly IEmailService _emailService;

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
        var result = await _emailService.TrySendAsync(new EmailData
        {
            Subject = "Reset Password",
            ToEmail = request.Email,
            HtmlBody = $"""
                        <div  style="text-align:center;">
                        <h3>
                        Reset Password
                        </h3>
                        <p>
                        click the button below to reset your password
                        </p>
                        <a href="http://localhost:5196/api/identity/reset-password-2?email={request.Email}&token={token}" target="_blank" style="width:64px;height:32px;border:1px solid black;border-radius:15px;background-color:blue;color:white;padding:10px;">
                        Reset Password
                        </a>
                        </div>
                        """
        });

        return result ? Result.Success() : Error.UnKnown;
    }
}