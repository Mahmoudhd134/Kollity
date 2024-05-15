using Kollity.Services.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kollity.Services.API.Hubs.Abstraction;

public class BaseHub<T> : Hub<T> where T : class
{
    private ISender _sender;
    private IUserServices _userServices;

    protected ISender Sender =>
        _sender ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<ISender>();

    protected IUserServices UserServices =>
        _userServices ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<IUserServices>();
}