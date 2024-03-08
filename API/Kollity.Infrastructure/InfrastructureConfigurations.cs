using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos.Email;
using Kollity.Infrastructure.BackgroundJobs;
using Kollity.Infrastructure.Emails;
using Kollity.Infrastructure.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Kollity.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IFileServices, PhysicalFileServices>();
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

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