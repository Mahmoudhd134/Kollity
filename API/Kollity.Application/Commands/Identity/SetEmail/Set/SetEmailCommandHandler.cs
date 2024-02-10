using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Email;
using Kollity.Application.Extensions;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Identity.SetEmail.Set;

public class SetEmailCommandHandler : ICommandHandler<SetEmailCommand>
{
    private readonly IEmailService _emailService;
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public SetEmailCommandHandler(UserManager<BaseUser> userManager, IEmailService emailService,
        IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _emailService = emailService;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(SetEmailCommand request, CancellationToken cancellationToken)
    {
        var id = _userAccessor.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var setResult = await _userManager.SetEmailAsync(user, request.Email);
        if (setResult.Succeeded == false)
            return setResult.Errors.ToAppError().ToList();

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var result = await _emailService.TrySendAsync(new EmailData
        {
            Subject = "Reset Password",
            ToEmail = user.Email,
            HtmlBody = $"""
                        <div  style="text-align:center;">
                        <h3>
                        Confirm Email
                        </h3>
                        <p>
                        click the button below to confirm your email
                        </p>
                        <a href="http://localhost:5196/api/identity/confirm-email?email={request.Email}&token={token}
                        Confirm Email
                        </a>
                        </div>
                        """
        });

        return result ? Result.Success() : Error.UnKnown;
    }
}