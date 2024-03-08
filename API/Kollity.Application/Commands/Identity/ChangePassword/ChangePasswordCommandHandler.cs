using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Extensions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Identity.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<BaseUser> userManager, IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
    {
        _userManager = userManager;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var userId = _userAccessor.GetCurrentUserId();
=======
        var userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return UserErrors.IdNotFound(userId);

        var result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDto.OldPassword,
            request.ChangePasswordDto.NewPassword);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}