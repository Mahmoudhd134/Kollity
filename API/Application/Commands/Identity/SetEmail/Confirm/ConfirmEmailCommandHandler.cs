using Application.Abstractions;
using Application.Extensions;
using Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Identity.SetEmail.Confirm;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
{
    private readonly UserManager<BaseUser> _userManager;
    private readonly IUserAccessor _userAccessor;

    public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager,IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var id = _userAccessor.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        
        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}