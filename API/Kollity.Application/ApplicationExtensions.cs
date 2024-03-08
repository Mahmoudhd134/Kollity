<<<<<<< HEAD
﻿using Kollity.Application.Dtos.Email;
=======
﻿using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Email;
using Kollity.Application.Events;
using Kollity.Application.MediatorPipelines;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Application;

public static class ApplicationExtensions
{
<<<<<<< HEAD
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(opt =>
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions)));

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);


        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

=======
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions));

            opt.AddOpenBehavior(typeof(HandleEventsPipeline<,>));
        });

        services.AddScoped<EventCollection>();

        services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);

>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        return services;
    }
}