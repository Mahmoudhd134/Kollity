using Kollity.Application.Abstractions.Events;
using Kollity.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Kollity.Infrastructure.BackgroundJobs;

public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var outboxMessages = await _context.OutboxMessages
            .Where(x => x.ProcessedOn == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in outboxMessages)
        {
            var e = JsonConvert.DeserializeObject<IEvent>(outboxMessage.Content, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            if (e is null)
            {
                continue;
            }

            await _publisher.Publish(e, context.CancellationToken);

            outboxMessage.ProcessedOn = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}