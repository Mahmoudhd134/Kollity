using Kollity.API.Extensions;
using Kollity.API.Helpers;
using Kollity.Application.Commands.Identity.ChangeImagePhoto;
using Kollity.Application.Commands.Identity.ChangePassword;
using Kollity.Application.Commands.Identity.ResetPassword.Reset;
using Kollity.Application.Commands.Identity.ResetPassword.SendToken;
using Kollity.Application.Commands.Identity.SetEmail.Confirm;
using Kollity.Application.Commands.Identity.SetEmail.Set;
using Kollity.Application.Dtos.Identity;
using Kollity.Application.Dtos.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Exception = System.Exception;

namespace Kollity.API.Controllers;

public class IdentityController : BaseController
{
    [HttpPost("change-password")]
    public Task<IResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto) =>
        Send(new ChangePasswordCommand(changePasswordDto));

    [AllowAnonymous]
    [HttpPost("reset-password-1/{email}")]
    public Task<IResult> ResetPassword(string email) => Send(new SendResetPasswordTokenToEmailCommand(email));

    [AllowAnonymous]
    [HttpPost("reset-password-2")]
    public Task<IResult> ResetPassword2(ResetPasswordDto resetPasswordDto) =>
        Send(new ResetPasswordCommand(resetPasswordDto));

    [HttpPost("set-email")]
    public Task<IResult> SetEmail(string email) => Send(new SetEmailCommand(email));

    [HttpPost("confirm-email")]
    public Task<IResult> ConfirmEmail(string token) => Send(new ConfirmEmailCommand(token));

    [HttpPost("change-profile-image")]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    [DisableFormValueModelBinding]
    public async Task<IResult> ChangeImagePhoto()
    {
        var fileResult = await Request.GetSectionAndFileInfos(MaxFileSize);
        if (fileResult.IsSuccess == false)
            return fileResult.ToIResult();

        var (section, fileName) = fileResult.Data;

        await using var fileStream = section.Body;
        var result = await Send(new ChangeUserProfileImageCommand(new ChangeImagePhotoDto
        {
            ImageStream = fileStream,
            Extensions = "." + fileName.Split(".").Last()
        }));
        return result;
    }
}