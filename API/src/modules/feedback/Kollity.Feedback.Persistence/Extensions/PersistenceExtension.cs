using Kollity.Feedback.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Feedback.Persistence.Extensions;

public static class PersistenceConfiguration
{
    
    public static IServiceCollection AddFeedbackPersistenceConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["ConnectionStrings:FeedbackDatabase"];
        services.AddDbContext<FeedbackDbContext>(opt =>
            opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "feedback")));

        return services;
    }
}