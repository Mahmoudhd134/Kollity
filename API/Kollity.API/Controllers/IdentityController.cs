using Kollity.Application.Commands.Identity.ChangeImagePhoto;
using Kollity.Application.Commands.Identity.ChangePassword;
using Kollity.Application.Commands.Identity.ResetPassword.Reset;
using Kollity.Application.Commands.Identity.ResetPassword.SendToken;
using Kollity.Application.Commands.Identity.SetEmail.Confirm;
using Kollity.Application.Commands.Identity.SetEmail.Set;
using Kollity.Application.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kollity.API.Controllers;

public class IdentityController : BaseController
{
    [HttpPost("change-password")]
    public Task<IResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        return Send(new ChangePasswordCommand(changePasswordDto));
    }

    [AllowAnonymous]
    [HttpPost("reset-password-1/{email}")]
    public Task<IResult> ResetPassword(string email)
    {
        return Send(new SendResetPasswordTokenToEmailCommand(email));
    }

    [AllowAnonymous]
    [HttpPost("reset-password-2")]
    public Task<IResult> ResetPassword2(ResetPasswordDto resetPasswordDto)
    {
        return Send(new ResetPasswordCommand(resetPasswordDto));
    }

    [HttpPost("set-email")]
    public Task<IResult> SetEmail(string email)
    {
        return Send(new SetEmailCommand(email));
    }

    [HttpPost("confirm-email")]
    public Task<IResult> ConfirmEmail(string token)
    {
        return Send(new ConfirmEmailCommand(token));
    }

    [HttpPost("change-image-photo")]
    public Task<IResult> ChangeImagePhoto([FromForm] ChangeImagePhotoDto photoDto)
    {
        return Send(new ChangeUserProfileImageCommand(photoDto));
    }
}