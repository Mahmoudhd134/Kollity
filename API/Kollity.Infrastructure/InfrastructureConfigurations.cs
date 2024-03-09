using Kollity.Application.Abstractions.Services;
using Kollity.Infrastructure.BackgroundJobs;
using Kollity.Infrastructure.Files;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Kollity.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IFileServices, PhysicalFileServices>();

        services.AddQuartzConfig();

        return services;
    }

    public static IServiceCollection AddQuartzConfig(this IServiceCollection services)
    {
        services.AddQuartz(config =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            config
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule
                        .WithIntervalInMinutes(1)
                        .RepeatForever()));
        });

        services.AddQuartzHostedService();

        return services;
    }
}