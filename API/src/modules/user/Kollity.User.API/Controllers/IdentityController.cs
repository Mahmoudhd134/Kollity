using Kollity.User.API.Dtos.Identity;
using Kollity.User.API.Mediator.Identity.ChangeImagePhoto;
using Kollity.User.API.Mediator.Identity.ChangePassword;
using Kollity.User.API.Mediator.Identity.ResetPassword.Reset;
using Kollity.User.API.Mediator.Identity.ResetPassword.SendToken;
using Kollity.User.API.Mediator.Identity.SetEmail.Confirm;
using Kollity.User.API.Mediator.Identity.SetEmail.Set;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kollity.User.API.Controllers;

public class IdentityController : BaseController
{
    [HttpPatch("change-password")]
    public Task<IResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        return Send(new ChangePasswordCommand(changePasswordDto));
    }

    [AllowAnonymous]
    [HttpPatch("forget-password/{email}")]
    public Task<IResult> ResetPassword(string email)
    {
        return Send(new SendResetPasswordTokenToEmailCommand(email));
    }

    [AllowAnonymous]
    [HttpPatch("reset-password")]
    public Task<IResult> ResetPassword2(ResetPasswordDto resetPasswordDto)
    {
        return Send(new ResetPasswordCommand(resetPasswordDto));
    }

    [HttpPatch("set-email")]
    public Task<IResult> SetEmail(string email)
    {
        return Send(new SetEmailCommand(email));
    }

    [HttpPatch("confirm-email")]
    public Task<IResult> ConfirmEmail(string token)
    {
        return Send(new ConfirmEmailCommand(token));
    }

    [HttpPatch("change-profile-image")]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    public async Task<IResult> Change([FromForm] ImageDto dto)
    {
        var stream = dto.Image.OpenReadStream();
        var result = await Send(new ChangeUserProfileImageCommand(new ChangeImagePhotoDto
        {
            ImageStream = stream,
            Extensions = "." + dto.Image.FileName.Split(".").Last()
        }));
        stream.Close();
        return result;
    }
}

public class ImageDto
{
    public IFormFile Image { get; set; }
}