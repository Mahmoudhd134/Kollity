﻿using Kollity.Common.Abstractions.Messages;
using Kollity.Common.ErrorHandling;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Kollity.User.API.Mediator.Identity.SetEmail.Confirm;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand, string>
{
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<BaseUser> userManager, IUserServices userServices)
    {
        _userManager = userManager;
        _userServices = userServices;
    }

    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var id = _userServices.GetCurrentUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return UserErrors.IdNotFound(id);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        return result.Succeeded
            ? user.Email
            : result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();
    }
}