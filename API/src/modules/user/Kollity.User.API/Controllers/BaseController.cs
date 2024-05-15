using System.Security.Claims;
using Kollity.Common.Abstractions.Messages;
using Kollity.User.API.Extensions;
using Kollity.User.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kollity.User.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[SwaggerResponse(400, type: typeof(UserFailureType))]
[SwaggerResponse(404, type: typeof(UserFailureType))]
[SwaggerResponse(409, type: typeof(UserFailureType))]
[SwaggerResponse(500, type: typeof(UserFailureType))]
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
        return (await Sender.Send(command)).ToIResult();
    }
}