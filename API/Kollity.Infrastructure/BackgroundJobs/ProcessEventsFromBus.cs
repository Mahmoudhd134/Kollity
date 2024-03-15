using Kollity.Application.Abstractions.Events;
using Kollity.Application.Extensions;
using Kollity.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kollity.Infrastructure.BackgroundJobs;

public class ProcessEventsFromBus : BackgroundService
{
    private readonly IBus _bus;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessEventsFromBus(IBus bus, IServiceScopeFactory serviceScopeFactory)
    {
        _bus = bus;
        _serviceScopeFactory = serviceScopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        await foreach (var eventWithId in _bus.ConsumeAllAsync(stoppingToken))
        {
            try
            {
                await publisher.Publish(eventWithId.Event, stoppingToken);
                await context.OutboxMessages
                    .Where(x => x.Id == eventWithId.Id)
                    .ExecuteUpdateAsync(c => c
                            .SetProperty(x => x.ProcessedOn, DateTime.UtcNow)
                        , stoppingToken);
            }
            catch (Exception exception)
            {
                await context.OutboxMessages
                    .Where(x => x.Id == eventWithId.Id)
                    .ExecuteUpdateAsync(c => c
                            .SetProperty(x => x.ProcessedOn, DateTime.UtcNow)
                            .SetProperty(x => x.Error, exception.GetErrorMessage())
                        , stoppingToken);
            }
        }
    }
}