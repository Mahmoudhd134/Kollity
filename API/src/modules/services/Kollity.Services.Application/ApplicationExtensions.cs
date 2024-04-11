using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.MediatorPipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Services.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServicesApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions));

            opt.AddOpenBehavior(typeof(HandleEventsPipeline<,>));
        });

        services.AddScoped<EventCollection>();

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);

        return services;
    }
}