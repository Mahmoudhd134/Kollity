using System.Security.Claims;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using Application.Abstractions.Messages;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[
    ApiController,
    Route("api/[controller]"),
    SwaggerResponse(400, type: typeof(FailureType)),
    SwaggerResponse(404, type: typeof(FailureType)),
    SwaggerResponse(409, type: typeof(FailureType)),
    SwaggerResponse(500, type: typeof(FailureType)),
]
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

    protected async Task<IResult> Send<T>(IQuery<T> query) =>
        (await Sender.Send(query)).ToIResult();

    protected async Task<IResult> Send(ICommand command) =>
        (await Sender.Send(command)).ToIResult();

    protected async Task<IResult> Send<T>(ICommand<T> command) =>
        (await Sender.Send(command)).ToIResult();
}