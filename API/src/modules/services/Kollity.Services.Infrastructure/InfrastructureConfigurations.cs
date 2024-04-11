using Kollity.Services.Application;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Contracts.Student;
using Kollity.Services.Infrastructure.Files;
using Kollity.Services.Infrastructure.Implementation.Email;
using Kollity.Services.Infrastructure.Implementation.IntegrationServices;
using Kollity.Services.Infrastructure.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kollity.Services.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddServicesInfrastructureConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IFileServices, PhysicalFileServices>();
        services.AddScoped<IUserServiceServices, UserServiceServices>();


        services.AddTransient<IEventBus, EventBus>();

        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(InfrastructureConfigurations));
        });

        // services.AddMassTransitConfiguration(configuration);

        return services;
    }

    private static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection("Queues:RabbitMq"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqConfig>>().Value);
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.AddConsumers(
                typeof(ApplicationExtensions).Assembly
            );

            busConfig.UsingRabbitMq((context, config) =>
            {
                config.ConfigureEndpoints(context);
                var rabbitMqConfig = context.GetRequiredService<RabbitMqConfig>();
                config.Host(new Uri(rabbitMqConfig.Host), host =>
                {
                    host.Username(rabbitMqConfig.Username);
                    host.Password(rabbitMqConfig.Password);
                });
            });
        });
        return services;
    }
}