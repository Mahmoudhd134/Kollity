using Kollity.Application.Abstractions.Events;
using MediatR;

namespace Kollity.Infrastructure.Abstraction.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent;