using MediatR;

namespace Kollity.Application.Abstractions.Events;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent;