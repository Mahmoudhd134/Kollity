using System.Text;
using Kollity.Exams.Api.Helpers;
using Kollity.Exams.Api.Implementation;
using Kollity.Exams.Application;
using Kollity.Exams.Application.Abstractions.Services;
using Kollity.Exams.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kollity.Exams.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExamsApiServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, HttpUserServices>();
        services.AddHttpContextAccessor();

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
        });
        return services;
    }
}