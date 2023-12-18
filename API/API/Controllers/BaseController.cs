using System.Security.Claims;
using API.Extensions;
using Application.Abstractions.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private ISender _sender;
    private ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected string Username => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    protected string Id => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;
    protected string UserAgent => HttpContext.Request.Headers.UserAgent;

    protected IList<string> Roles => User?.Claims?
        .Where(c => c.Type.Equals(ClaimTypes.Role))
        .Select(c => c.Value)
        .ToList();

    protected async Task<IResult> Send(ICommand command)
    {
        return (await Sender.Send(command)).ToIResult();
    }

    protected async Task<IResult> Send<T>(ICommand<T> command)
    {
        return (await Sender.Send(command)).ToIResult();
    }
}