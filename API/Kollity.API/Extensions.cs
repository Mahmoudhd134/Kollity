using System.Text;
using Kollity.API.Helpers;
using Kollity.Services.API.Hubs;
using Kollity.Services.Application;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using KollityUserApiEntryPoint = Kollity.User.API.Extensions.ServiceCollectionExtensions;
using KollityServicesApiEntryPoint = Kollity.Services.API.Extensions.ServiceCollectionExtensions;


namespace Kollity.API;

public static class Extensions
{
    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services,
        IConfiguration configuration, bool isProductionEnvironment)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection("Queues:RabbitMq"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqConfig>>().Value);
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.AddConsumers(
                typeof(ApplicationExtensions).Assembly,
                typeof(KollityUserApiEntryPoint).Assembly
            );

            if (isProductionEnvironment)
            {
                busConfig.UsingRabbitMq((context, config) =>
                {
                    config.ConfigureEndpoints(context);
                    var rabbitMqConfig = context.GetRequiredService<RabbitMqConfig>();
                    config.Host(new Uri(rabbitMqConfig.Host), host =>
                    {
                        host.Username(rabbitMqConfig.Username);
                        host.Password(rabbitMqConfig.Password);
                    });
                });
            }
            else
            {
                busConfig.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
            }
        });
        return services;
    }

    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kollity.Services.API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            c.AddSignalRSwaggerGen(o =>
            {
                o.ScanAssemblies([
                    typeof(KollityServicesApiEntryPoint).Assembly,
                    typeof(KollityUserApiEntryPoint).Assembly
                ]);
            });
        });
        return services;
    }

    public static IServiceCollection AddModelBindingErrorsMap(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(CustomValidationFilterAttribute));
            // opt.Filters.Add(new DisableFormValueModelBindingAttribute());
        });
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Query["access_token"];
                        var isHubPath = context.Request.Path.StartsWithSegments("/" + HubConfigurations.BaseHubPath);
                        if (!string.IsNullOrWhiteSpace(token) && isHubPath)
                            context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    public static IServiceCollection AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(opt => opt.AddPolicy("allowLocalInDevelopment", builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:5173",
                    "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        }));

        return services;
    }
}