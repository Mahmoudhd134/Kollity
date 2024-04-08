using MediatR;

namespace Kollity.Services.Application.Abstractions.Events;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent;