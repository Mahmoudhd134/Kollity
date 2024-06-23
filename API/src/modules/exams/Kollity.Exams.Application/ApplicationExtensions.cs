using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Application.MediatorPipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Exams.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddExamsApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions));

            opt.AddOpenBehavior(typeof(HandelTransactionsPipeline<,>));
            opt.AddOpenBehavior(typeof(HandleEventsPipeline<,>));
        });

        services.AddScoped<EventCollection>();

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);

        return services;
    }
}