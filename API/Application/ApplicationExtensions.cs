using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions)));

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);
        return services;
    }
}