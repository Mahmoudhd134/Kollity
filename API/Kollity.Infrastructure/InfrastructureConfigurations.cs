using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Messages;
using Kollity.Application.Abstractions.Services;
using Kollity.Infrastructure.Files;
using Kollity.Infrastructure.Implementation;
using Kollity.Infrastructure.Implementation.Email;
using Kollity.Infrastructure.Messages;
using MassTransit;
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


        services.AddTransient<IEventBus, EventBus>();

        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(InfrastructureConfigurations));
        });

        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            busConfig.UsingInMemory();
        });

        return services;
    }
}