namespace Kollity.Services.Application.Abstractions.Messages;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}