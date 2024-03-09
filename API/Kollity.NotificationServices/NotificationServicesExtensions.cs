using Kollity.NotificationServices.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.NotificationServices;

public static class NotificationServicesExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(NotificationServicesExtensions));
        });
        return services;
    }
}