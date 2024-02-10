using Kollity.Application.Abstractions;
using Kollity.Application.Extensions;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Identity.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<BaseUser> userManager, IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return UserErrors.IdNotFound(userId);

        var result = await _userManager.ChangePasswordAsync(user, request.ChangePasswordDto.OldPassword,
            request.ChangePasswordDto.NewPassword);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}