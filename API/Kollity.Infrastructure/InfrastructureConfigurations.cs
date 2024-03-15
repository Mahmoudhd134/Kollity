using System.Threading.Channels;
using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Services;
using Kollity.Infrastructure.BackgroundJobs;
using Kollity.Infrastructure.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IFileServices, PhysicalFileServices>();


        var channel = Channel.CreateUnbounded<EventWithId>();
        var bus = new Bus(channel);
        services.AddSingleton<IBus>(bus);

        services.AddHostedService<ProcessEventsFromBus>();
        services.AddHostedService<ProcessUnProcessedEvents>();

        return services;
    }
}