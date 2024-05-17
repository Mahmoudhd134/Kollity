using Kollity.Common.Abstractions.Messages;
using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Mediator.Identity.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
    {
        _userManager = userManager;
        _userServices = userServices;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _userServices.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return UserErrors.IdNotFound(userId);

        var result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDto.OldPassword,
            request.ChangePasswordDto.NewPassword);

        return result.Succeeded
            ? Result.Success()
            : result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();
    }
}