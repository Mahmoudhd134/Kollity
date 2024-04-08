using System.Globalization;
using System.Security.Claims;
using Kollity.Services.API.Extensions;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.Services.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[SwaggerResponse(400, type: typeof(FailureType))]
[SwaggerResponse(404, type: typeof(FailureType))]
[SwaggerResponse(409, type: typeof(FailureType))]
[SwaggerResponse(500, type: typeof(FailureType))]
public class BaseController : ControllerBase
{
    protected const long MaxFileSize = 1L * 1024L * 1024L * 1024L;
    private ISender _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected string Username => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    protected string Id => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;
    protected string UserAgent => HttpContext.Request.Headers.UserAgent;

    protected IList<string> Roles => User?.Claims?
        .Where(c => c.Type.Equals(ClaimTypes.Role))
        .Select(c => c.Value)
        .ToList();

    protected async Task<IResult> Send<T>(IQuery<T> query)
    {
        return (await Sender.Send(query)).ToIResult();
    }

    protected async Task<IResult> Send(ICommand command)
    {
        return (await Sender.Send(command)).ToIResult();
    }

    protected async Task<IResult> Send<T>(ICommand<T> command)
    {
        return ((Result<T>)(await Sender.Send(command))).ToIResult();
    }

    protected async Task CopyFileToResponse(FileStreamDto fileStreamDto)
    {
        Response.Headers.Append("Content-Disposition",
            "attachment; filename=" + fileStreamDto.Name + fileStreamDto.Extension);
        Response.Headers.Append("Content-Type", "application/octet-stream");
        Response.Headers.Append("Content-Length", fileStreamDto.Size.ToString(CultureInfo.InvariantCulture));
        await fileStreamDto.Stream.CopyToAsync(Response.Body);
        fileStreamDto.Stream.Close();
    }
}