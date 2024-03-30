using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Messages;
using Kollity.Application.Abstractions.Services;
using Kollity.Infrastructure.Abstraction.Email;
using Kollity.Infrastructure.BackgroundJobs;
using Kollity.Infrastructure.Files;
using Kollity.Infrastructure.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IFileServices, PhysicalFileServices>();


        services.AddSingleton<InMemoryChannel>();
        services.AddSingleton<IBus, Bus>();

        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(InfrastructureConfigurations));
        });

        services.AddHostedService<ProcessEventsFromBus>();
        services.AddHostedService<ProcessUnProcessedEvents>();

        return services;
    }
}