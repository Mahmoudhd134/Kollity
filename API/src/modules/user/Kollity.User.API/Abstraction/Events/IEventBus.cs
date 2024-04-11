namespace Kollity.User.API.Abstraction.Events;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}