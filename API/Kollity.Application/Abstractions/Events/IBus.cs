using Kollity.Contracts.Events;

namespace Kollity.Application.Abstractions.Events;

public interface IBus : IDisposable
{
    Task PublishAsync(EventWithId eventWithId, CancellationToken cancellationToken);
    IAsyncEnumerable<EventWithId> ConsumeAllAsync(CancellationToken cancellationToken);
}