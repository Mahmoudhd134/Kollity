﻿using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Email;
using Kollity.Application.Events;
using Kollity.Application.MediatorPipelines;
using Microsoft.Extensions.Configuration;
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