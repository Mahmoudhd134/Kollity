using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Extensions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Identity.SetEmail.Confirm;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
{
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
    {
        _userManager = userManager;
        _userServices = userServices;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}