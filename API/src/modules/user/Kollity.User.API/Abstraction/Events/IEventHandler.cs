using MediatR;

namespace Kollity.User.API.Abstraction.Events;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent;