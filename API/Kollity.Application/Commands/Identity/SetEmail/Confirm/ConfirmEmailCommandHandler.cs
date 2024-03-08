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

namespace Kollity.Application.Commands.Identity.SetEmail.Confirm;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
{
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager, IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
    {
        _userManager = userManager;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}