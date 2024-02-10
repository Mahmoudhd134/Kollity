using Kollity.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kollity.API.Hubs;

public class BaseHub<T> : Hub<T> where T : class
{
    private ISender _sender;
    private IUserAccessor _userAccessor;

    protected ISender Sender =>
        _sender ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<ISender>();

    protected IUserAccessor UserAccessor =>
        _userAccessor ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<IUserAccessor>();
}