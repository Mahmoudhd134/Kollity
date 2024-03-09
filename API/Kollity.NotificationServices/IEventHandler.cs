using Kollity.Contracts.Events;
using MediatR;

namespace Kollity.NotificationServices;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}