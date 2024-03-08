using Kollity.API.Extensions;
using Kollity.API.Helpers;
using Kollity.Application.Commands.Identity.ChangeImagePhoto;
using Kollity.Application.Commands.Identity.ChangePassword;
<<<<<<< HEAD
=======
using Kollity.Application.Commands.Identity.EditSettings;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Commands.Identity.ResetPassword.Reset;
using Kollity.Application.Commands.Identity.ResetPassword.SendToken;
using Kollity.Application.Commands.Identity.SetEmail.Confirm;
using Kollity.Application.Commands.Identity.SetEmail.Set;
using Kollity.Application.Dtos.Identity;
<<<<<<< HEAD
=======
using Kollity.Application.Queries.Identity.GetMySettings;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
<<<<<<< HEAD
=======
using Swashbuckle.AspNetCore.Annotations;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

namespace Kollity.API.Controllers;

public class IdentityController : BaseController
{
<<<<<<< HEAD
=======
    [HttpGet("settings"), SwaggerResponse(200, type: typeof(UserSettingsDto))]
    public Task<IResult> GetSettings()
    {
        return Send(new GetMyUserSettingsQuery());
    }

>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    [HttpPatch("change-password")]
    public Task<IResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        return Send(new ChangePasswordCommand(changePasswordDto));
    }

    [AllowAnonymous]
    [HttpPatch("reset-password-1/{email}")]
    public Task<IResult> ResetPassword(string email)
    {
        return Send(new SendResetPasswordTokenToEmailCommand(email));
    }

    [AllowAnonymous]
    [HttpPatch("reset-password-2")]
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

<<<<<<< HEAD
=======
    [HttpPatch("edit-settings")]
    public Task<IResult> EditSettings(UserSettingsDto dto)
    {
        return Send(new EditMyUserSettingsCommand(dto));
    }

>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    [HttpPatch("change-profile-image")]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    [DisableFormValueModelBinding]
    public async Task<IResult> ChangeImagePhoto()
    {
        if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            return Result.Failure(Error.Validation("UploadFile", "Not a multipart request")).ToIResult();

        var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), MaxFileSize);
        if (boundary.IsSuccess == false)
            return Result.Failure(boundary.Errors).ToIResult();
        var reader = new MultipartReader(boundary.Data, Request.Body);

        // note: this is for a single file, you could also process multiple files
        MultipartSection section;
        ContentDispositionHeaderValue contentDisposition = null;
        do
        {
            section = await reader.ReadNextSectionAsync();

            if (section == null)
                break;

            if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition))
                return Result.Failure(Error.Validation("UploadFile", "No content disposition in multipart defined"))
                    .ToIResult();
        } while (contentDisposition.Name.ToString().ToLower() != "image");

        if (section is null)
            return Result.Failure(Error.Validation("Image", "Image Is Required.")).ToIResult();
        if (contentDisposition?.IsFileDisposition() == false)
            return Result.Failure(Error.Validation("Image", "Image Must Be a File.")).ToIResult();

        var fileName = contentDisposition.FileNameStar.ToString();
        if (string.IsNullOrEmpty(fileName)) fileName = contentDisposition.FileName.ToString();

        if (string.IsNullOrEmpty(fileName))
            return Result.Failure(Error.Validation("UploadFile", "No filename defined.")).ToIResult();


        await using var fileStream = section.Body;
        var result = await Send(new ChangeUserProfileImageCommand(new ChangeImagePhotoDto
        {
            ImageStream = fileStream,
            Extensions = "." + fileName.Split(".").Last()
        }));
        return result;
    }

    [HttpPatch("change-profile-image-non-streaming")]
    [RequestSizeLimit(MaxFileSize)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
    public async Task<IResult> Change([FromForm] ImageDto dto)
    {
        var stream = dto.Image.OpenReadStream();
        var result = await Send(new ChangeUserProfileImageCommand(new ChangeImagePhotoDto()
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