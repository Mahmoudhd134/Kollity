using MassTransit;

namespace Kollity.Exams.Application.Abstractions.Events;

public abstract class IntegrationEventConsumer<T> : IConsumer<T> where T : class
{
    protected abstract Task Handle(T integrationEvent);

    public Task Consume(ConsumeContext<T> context)
    {
        return Handle(context.Message);
    }
}