using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Events;
using Kollity.Domain.Messages;
using MediatR;
using Newtonsoft.Json;

namespace Kollity.Application.MediatorPipelines;

public class HandleEventsPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandWithEvents
{
    private readonly EventCollection _eventCollection;
    private readonly ApplicationDbContext _context;
    private readonly IBus _bus;

    public HandleEventsPipeline(EventCollection eventCollection, ApplicationDbContext context,
        IBus bus)
    {
        _eventCollection = eventCollection;
        _context = context;
        _bus = bus;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (_eventCollection.Any() == false)
            return response;

        var events = _eventCollection.Events();
        _eventCollection.Clear();
        var outboxMessages = events.Select(x => new OutboxMessage
            {
                Type = x.GetType().Name,
                Content = JsonConvert.SerializeObject(x, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }),
                OccuredOn = DateTime.UtcNow
            })
            .ToList();
        _context.OutboxMessages.AddRange(outboxMessages);
        await _context.SaveChangesAsync(cancellationToken);


        for (var i = 0; i < events.Count; i++)
        {
            await _bus.PublishAsync(new EventWithId(events[i], outboxMessages[i].Id), cancellationToken);
        }

        return response;
    }
}