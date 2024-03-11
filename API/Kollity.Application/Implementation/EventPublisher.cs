using Kollity.Application.Abstractions;
using Kollity.Application.Extensions;
using Kollity.Contracts.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Implementation;

public class EventPublisher : IEventPublisher
{
    private readonly IPublisher _publisher;
    private readonly ApplicationDbContext _context;

    public EventPublisher(IPublisher publisher, ApplicationDbContext context)
    {
        _publisher = publisher;
        _context = context;
    }

    public async Task PublishAsync<T>(T e, Guid eventId, CancellationToken cancellationToken = default) where T : IEvent
    {
        try
        {
            await _publisher.Publish(e, cancellationToken);
            await _context.OutboxMessages
                .Where(x => x.Id == eventId)
                .ExecuteUpdateAsync(c => c
                        .SetProperty(x => x.ProcessedOn, DateTime.UtcNow)
                    , cancellationToken);
        }
        catch (Exception exception)
        {
            await _context.OutboxMessages
                .Where(x => x.Id == eventId)
                .ExecuteUpdateAsync(c => c
                        .SetProperty(x => x.ProcessedOn, DateTime.UtcNow)
                        .SetProperty(x => x.Error, exception.GetErrorMessage())
                    , cancellationToken);
        }
    }
}