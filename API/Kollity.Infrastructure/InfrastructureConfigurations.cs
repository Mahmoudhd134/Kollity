using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Infrastructure.Emails;
using Kollity.Infrastructure.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProfileImageAccessor, PhysicalProfileImageAccessor>();
        services.AddScoped<IFileAccessor, PhysicalFileAccessor>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}