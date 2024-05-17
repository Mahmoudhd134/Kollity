using System.Text;
using Kollity.Services.API.Helpers;
using Kollity.Services.API.Hubs;
using Kollity.Services.API.Hubs.Abstraction;
using Kollity.Services.API.Hubs.Implementations;
using Kollity.Services.API.Implementation;
using Kollity.Services.Application;
using Kollity.Services.Application.Abstractions.RealTime;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Infrastructure;
using Kollity.Services.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kollity.Services.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesApiConfigurations(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddServicesApplicationConfiguration();
        services.AddServicesPersistenceConfiguration();
        services.AddServicesInfrastructureConfiguration();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCustomSwaggerGen();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHealthChecks();
        services
            .AddCorsExtension()
            .AddJwtAuthentication(configuration)
            .AddClassesConfigurations(configuration)
            .AddServicesServicesInjection()
            .AddModelBindingErrorsMap()
            .AddSignalR();

        return services;
    }

    public static IServiceCollection AddServicesServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, HttpUserServices>();
        services.AddHttpContextAccessor();

        var roomConnectionServices = new SignalRRoomConnectionServices();
        services.AddSingleton<IRoomConnectionServices>(roomConnectionServices);
        services.AddSingleton<IRoomConnectionsServices>(roomConnectionServices);

        return services;
    }

    private static IServiceCollection AddClassesConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = "AuthServer",
                    ValidAudience = "AuthServer",
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

    private static IServiceCollection AddFallbackPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            {
                // options.AddPolicy("VerySecurePolicy", policy => { policy.RequireClaim("role", "admin"); });
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            }
        );
        return services;
    }

    private static IServiceCollection AddCorsExtension(this IServiceCollection services)
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

    private static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
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
            c.AddSignalRSwaggerGen();
        });
        return services;
    }

    private static IServiceCollection AddModelBindingErrorsMap(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(CustomValidationFilterAttribute));
            // opt.Filters.Add(new DisableFormValueModelBindingAttribute());
        });
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

        return services;
    }
}