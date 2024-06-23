using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Infrastructure.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Exams.Infrastructure;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddExamsInfrastructureConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddTransient<IEventBus, EventBus>();

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(InfrastructureConfigurations));
        });


        return services;
    }
}