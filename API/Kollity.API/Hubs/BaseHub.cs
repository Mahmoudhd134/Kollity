using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kollity.API.Hubs;

public class BaseHub<T> : Hub<T> where T : class
{
    private ISender _sender;
<<<<<<< HEAD
    private IUserAccessor _userAccessor;
=======
    private IUserServices _userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

    protected ISender Sender =>
        _sender ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<ISender>();

<<<<<<< HEAD
    protected IUserAccessor UserAccessor =>
        _userAccessor ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<IUserAccessor>();
=======
    protected IUserServices UserServices =>
        _userServices ??= Context.GetHttpContext()?.RequestServices.GetRequiredService<IUserServices>();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
}