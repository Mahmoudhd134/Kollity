using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.Messages;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Messages;
using MediatR;
using Newtonsoft.Json;

namespace Kollity.Services.Application.MediatorPipelines;

public class HandleEventsPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly EventCollection _eventCollection;
    private readonly IPublisher _publisher;

    public HandleEventsPipeline(EventCollection eventCollection, IPublisher publisher)
    {
        _eventCollection = eventCollection;
        _publisher = publisher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (_eventCollection.Any() == false)
            return response;

        var events = _eventCollection.Events();
        _eventCollection.Clear();
        foreach (var e in events)
        {
            await _publisher.Publish(e, cancellationToken);
        }

        return response;
    }
}