using Kollity.Application.Dtos.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(opt =>
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions)));

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);


        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        return services;
    }
}