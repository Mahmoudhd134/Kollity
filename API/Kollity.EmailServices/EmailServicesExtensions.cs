using System.Configuration;
using Kollity.EmailServices.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.EmailServices;

public static class EmailServicesExtensions
{
    public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(EmailServicesExtensions));
        });
        return services;
    }
}