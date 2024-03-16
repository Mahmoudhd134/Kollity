using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Abstractions.Messages;

public interface IBus : IDisposable
{
    Task PublishAsync(EventWithId eventWithId, CancellationToken cancellationToken);
    IAsyncEnumerable<EventWithId> ConsumeAllAsync(CancellationToken cancellationToken);
}