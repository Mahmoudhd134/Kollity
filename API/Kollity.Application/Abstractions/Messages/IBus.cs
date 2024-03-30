using Kollity.Application.Abstractions.Events;

namespace Kollity.Application.Abstractions.Messages;

public interface IBus
{
    Task PublishAsync(EventWithId eventWithId, CancellationToken cancellationToken);
}