using Kollity.Contracts.Events;
using MediatR;

namespace Kollity.EmailServices.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}