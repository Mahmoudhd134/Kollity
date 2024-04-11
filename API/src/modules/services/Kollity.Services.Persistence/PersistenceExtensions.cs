using Kollity.Services.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Services.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddServicesPersistenceConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["ConnectionStrings:ServicesDatabase"];
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

        // services.AddDefaultIdentity<BaseUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
        //     .AddRoles<BaseRole>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();
        //
        // services.AddIdentityCore<Student>()
        //     .AddRoles<BaseRole>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();
        //
        // services.AddIdentityCore<Doctor>()
        //     .AddRoles<BaseRole>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    // public static async Task UpdateDatabase(this WebApplication app)
    // {
    //     try
    //     {
    //         var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //         await context.Database.MigrateAsync();
    //     }
    //     catch
    //     {
    //         // ignored
    //     }
    // }
}