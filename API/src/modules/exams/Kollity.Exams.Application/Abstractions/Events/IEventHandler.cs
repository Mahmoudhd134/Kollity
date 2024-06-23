using MediatR;

namespace Kollity.Exams.Application.Abstractions.Events;

public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent;