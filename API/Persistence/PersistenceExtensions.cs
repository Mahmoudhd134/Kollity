using Domain.AssistantModels;
using Domain.DoctorModels;
using Domain.Identity.Role;
using Domain.Identity.User;
using Domain.StudentModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.EntityConfigurations.AssistantConfigurations;

namespace Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistenceConfigurations(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddDefaultIdentity<BaseUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
            .AddRoles<BaseRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityCore<Student>()
            .AddRoles<BaseRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityCore<Doctor>()
            .AddRoles<BaseRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        services.AddIdentityCore<Assistant>()
            .AddRoles<BaseRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    public static async Task UpdateDatabase(this WebApplication app)
    {
        try
        {
            var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }
        catch
        {
            // ignored
        }
    }
}