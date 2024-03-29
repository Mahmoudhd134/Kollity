﻿using Kollity.Application.Abstractions.Events;
using Kollity.Application.MediatorPipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
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