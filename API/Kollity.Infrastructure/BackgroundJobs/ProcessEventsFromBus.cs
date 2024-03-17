using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Messages;
using Kollity.Application.Extensions;
using Kollity.Domain.Messages;
using Kollity.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kollity.Infrastructure.BackgroundJobs;

public class ProcessEventsFromBus : BackgroundService
{
    private readonly IBus _bus;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProcessEventsFromBus> _logger;

    public ProcessEventsFromBus(IBus bus, IServiceScopeFactory serviceScopeFactory,
        ILogger<ProcessEventsFromBus> logger)
    {
        _bus = bus;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        Dictionary<Guid, DateTime> success = new();
        var timeThreshold = TimeSpan.FromSeconds(30);
        const int countThreshold = 10;

        await foreach (var eventWithId in _bus.ConsumeAllAsync(stoppingToken))
        {
            try
            {
                await publisher.Publish(eventWithId.Event, stoppingToken);
                // await context.OutboxMessages
                //     .Where(x => x.Id == eventWithId.Id)
                //     .ExecuteUpdateAsync(c => c
                //             .SetProperty(x => x.ProcessedOn, DateTime.UtcNow)
                //         , stoppingToken);
                success.Add(eventWithId.Id, DateTime.UtcNow);
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

            if (success.Count != countThreshold && DateTime.UtcNow - success.First().Value < timeThreshold)
                continue;

            var outboxMessages = success.Select(x => new OutboxMessage()
                {
                    Id = x.Key,
                    ProcessedOn = x.Value
                })
                .ToList();

            context.AttachRange(outboxMessages);
            outboxMessages.ForEach(outboxMessage =>
            {
                context.Entry(outboxMessage).Property(nameof(outboxMessage.ProcessedOn)).IsModified = true;
            });
            await context.SaveChangesAsync(stoppingToken);

            success.Clear();
        }
    }
}