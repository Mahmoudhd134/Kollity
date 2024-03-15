using Kollity.Application.Abstractions.Events;
using Kollity.Application.Extensions;
using Kollity.Contracts.Events;
using Kollity.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Infrastructure.BackgroundJobs;

public class EventPublisher 
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
    }
}