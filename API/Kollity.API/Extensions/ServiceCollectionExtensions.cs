using System.Text;
using Kollity.API.Abstractions;
using Kollity.API.Controllers;
using Kollity.API.Helpers;
using Kollity.API.Hubs;
using Kollity.API.Implementation;
using Kollity.Application.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kollity.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, JwtAuthServices>();
        services.AddScoped<IUserAccessor, HttpUserAccessor>();

        return services;
    }

    public static IServiceCollection AddClassesConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

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

    public static IServiceCollection AddFallbackPolicy(this IServiceCollection services)
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

    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kollity.API",
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
}