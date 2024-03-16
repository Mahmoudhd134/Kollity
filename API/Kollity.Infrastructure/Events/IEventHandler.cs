using Kollity.Application.Abstractions.Events;
using MediatR;

namespace Kollity.Infrastructure.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent;