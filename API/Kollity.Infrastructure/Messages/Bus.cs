using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Messages;

namespace Kollity.Infrastructure.Messages;

public class Bus : IBus
{
    private readonly InMemoryChannel _inMemoryChannel;

    public Bus(InMemoryChannel inMemoryChannel)
    {
        _inMemoryChannel = inMemoryChannel;
    }

    public async Task PublishAsync(EventWithId eventWithId, CancellationToken cancellationToken)
    {
        await _inMemoryChannel.Writer.WriteAsync(eventWithId, cancellationToken);
    }
}