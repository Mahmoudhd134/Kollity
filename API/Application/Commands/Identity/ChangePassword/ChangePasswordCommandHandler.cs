using Application.Abstractions;
using Application.Extensions;
using Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Identity.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly IUserAccessor _userAccessor;

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