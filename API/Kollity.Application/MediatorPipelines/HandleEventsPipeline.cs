using Kollity.Application.Events;
using Kollity.Domain.Messages;
using MediatR;
using Newtonsoft.Json;

namespace Kollity.Application.MediatorPipelines;

public class HandleEventsPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandWithEvents
{
    private readonly EventCollection _eventCollection;
    private readonly ApplicationDbContext _context;

    public HandleEventsPipeline(EventCollection eventCollection, ApplicationDbContext context)
    {
        _eventCollection = eventCollection;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (_eventCollection.Any() == false)
            return response;

        var events = _eventCollection.Events();
        _eventCollection.Clear();
        _context.OutboxMessages.AddRange(events.Select(x => new OutboxMessage
        {
            Type = x.GetType().Name,
            Content = JsonConvert.SerializeObject(x, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            }),
            OccuredOn = DateTime.UtcNow
        }));
        await _context.SaveChangesAsync(cancellationToken);
        return response;
    }
}