using Application.Commands.Identity.ChangePassword;
using Application.Commands.Identity.ResetPassword.Reset;
using Application.Commands.Identity.ResetPassword.SendToken;
using Application.Commands.Identity.SetEmail.Confirm;
using Application.Commands.Identity.SetEmail.Set;
using Application.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class IdentityController : BaseController
{
    [HttpPost("change-password")]
    public Task ChangePassword([FromBody] ChangePasswordDto changePasswordDto) =>
        Send(new ChangePasswordCommand(changePasswordDto));

    [AllowAnonymous, HttpPost("reset-password-1/{email}")]
    public Task ResetPassword(string email) => Send(new SendResetPasswordTokenToEmailCommand(email));

    [AllowAnonymous, HttpPost("reset-password-2")]
    public Task ResetPassword2(ResetPasswordDto resetPasswordDto) => Send(new ResetPasswordCommand(resetPasswordDto));

    [HttpPost("set-email")]
    public Task SetEmail(string email) => Send(new SetEmailCommand(email));

    [HttpPost("confirm-email")]
    public Task ConfirmEmail(string token) => Send(new ConfirmEmailCommand(token));
}