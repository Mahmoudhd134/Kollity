using System.Text;
using API.Abstractions;
using API.Helpers;
using API.Implementation;
using Application.Abstractions.Files;
using Infrastructure.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, JwtAuthServices>();
        services.AddScoped<IImageAccessor, PhysicalImageAccessor>();

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
                opt.TokenValidationParameters =
                    Constants.GetTokenValidationParameters(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)));
            });

        return services;
    }

    public static IServiceCollection AddFallbackPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            {
                options.AddPolicy("VerySecurePolicy", policy => { policy.RequireClaim("role", "admin"); });
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            }
        );
        return services;
    }

    public static IServiceCollection AddCores(this IServiceCollection services)
    {
        services.AddCors(opt => opt.AddPolicy("allowLocalInDevelopment", builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:5173",
                    "http://localhost:5174",
                    "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        }));

        return services;
    }

    public static IServiceCollection AddAuthToSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
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
        });
        return services;
    }
}