using MediatR;

namespace Kollity.Application.Abstractions.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}