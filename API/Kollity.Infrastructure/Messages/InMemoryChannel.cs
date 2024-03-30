using System.Threading.Channels;
using Kollity.Application.Abstractions.Events;

namespace Kollity.Infrastructure.Messages;

public class InMemoryChannel
{
    private readonly Channel<EventWithId> _channel = Channel.CreateUnbounded<EventWithId>();
    public ChannelWriter<EventWithId> Writer => _channel.Writer;
    public ChannelReader<EventWithId> Reader => _channel.Reader;
}