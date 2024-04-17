using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Reporting.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddServicesPersistenceConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["ConnectionStrings:ReportingDatabase"];
        services.AddDbContext<ReportingDbContext>(opt =>
            opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "reporting")));

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