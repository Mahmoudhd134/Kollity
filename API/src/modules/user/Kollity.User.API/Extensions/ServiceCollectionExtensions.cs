using System.Text;
using Kollity.User.API.Abstraction;
using Kollity.User.API.Abstraction.Events;
using Kollity.User.API.Abstraction.Services;
using Kollity.User.API.Data;
using Kollity.User.API.Helpers;
using Kollity.User.API.Models;
using Kollity.User.API.Services;
using Kollity.User.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kollity.User.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserApiConfigurations(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCustomSwaggerGen();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHealthChecks();
        services
            .AddUserDatabaseConfig()
            // .AddMassTransitConfigurations(configuration)
            .AddCorsExtension()
            .AddJwtAuthentication(configuration)
            .AddUserClassesConfigurations(configuration)
            .AddUserServicesInjection()
            .AddModelBindingErrorsMap()
            .AddSignalR();

        return services;
    }

    public static IServiceCollection AddUserServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, JwtAuthServices>();
        services.AddScoped<IUserIntegrationServices, UserIntegrationServices>();
        services.AddScoped<IProfileImageServices, PhysicalProfileImageServices>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserServices, HttpUserServices>();
        services.AddScoped<IEventBus, EventBus>();

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

        return services;
    }

    public static IServiceCollection AddUserClassesConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
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
                    ValidateAudience = false,
                    ValidateIssuer = false,
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

    public static IServiceCollection AddUserDatabaseConfig(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["ConnectionStrings:UserDatabase"];
        services.AddDbContext<UserDbContext>(opt =>
            opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "user")));

        services.AddDefaultIdentity<BaseUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserDbContext>();

        return services;
    }

    private static IServiceCollection AddMassTransitConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection("Queues:RabbitMq"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqConfig>>().Value);
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.AddConsumers(typeof(Program).Assembly);

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
        });
        return services;
    }
}