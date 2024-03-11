using Kollity.Contracts.Events;

namespace Kollity.Application.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync<T>(T e, Guid eventId, CancellationToken cancellationToken = default) where T : IEvent;
}