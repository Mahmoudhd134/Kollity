using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Messages;

namespace Kollity.Infrastructure.Messages;

public class Bus : IBus
{
    private readonly Channel<EventWithId> _channel;

    public Bus(Channel<EventWithId> channel)
    {
        _channel = channel;
    }

    public async Task PublishAsync(EventWithId eventWithId, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(eventWithId, cancellationToken);
    }

    public async IAsyncEnumerable<EventWithId> ConsumeAllAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var eventWithId in _channel.Reader.ReadAllAsync(cancellationToken))
        {
            yield return eventWithId;
        }
    }

    public void Dispose()
    {
        _channel.Writer.Complete();
    }
}