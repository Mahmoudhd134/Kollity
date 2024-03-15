using Kollity.Application.Abstractions.Events;
using Kollity.Contracts.Events;
using Kollity.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Kollity.Infrastructure.BackgroundJobs;

public class ProcessUnProcessedEvents : BackgroundService
{
    private readonly IBus _bus;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessUnProcessedEvents(IServiceScopeFactory serviceScopeFactory, IBus bus)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var events = (await context.OutboxMessages
                .Where(x => x.ProcessedOn == null)
                .Select(x => new
                {
                    x.Id,
                    x.Content
                })
                .ToListAsync(stoppingToken))
            .Select(x => new
            {
                x.Id,
                Event = JsonConvert.DeserializeObject<IEvent>(x.Content, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            }).ToList();

        foreach (var e in events)
        {
            await _bus.PublishAsync(new EventWithId(e.Event, e.Id), stoppingToken);
        }
    }
}