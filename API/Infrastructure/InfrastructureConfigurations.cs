using Application.Abstractions;
using Application.Abstractions.Files;
using Infrastructure.Emails;
using Infrastructure.Files;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IImageAccessor, PhysicalImageAccessor>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}