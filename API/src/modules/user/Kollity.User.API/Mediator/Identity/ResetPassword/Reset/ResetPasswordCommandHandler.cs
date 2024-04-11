using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction.Messages;
using Kollity.User.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Mediator.Identity.ResetPassword.Reset;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly UserManager<BaseUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<BaseUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
        if (user is null)
            return UserErrors.EmailNotFound(request.ResetPasswordDto.Email);

        var result = await _userManager.ResetPasswordAsync(user, request.ResetPasswordDto.Token,
            request.ResetPasswordDto.NewPassword);

        return result.Succeeded
            ? Result.Success()
            : result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();
    }
}