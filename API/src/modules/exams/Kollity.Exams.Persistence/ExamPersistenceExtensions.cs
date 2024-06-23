using Kollity.Exams.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kollity.Exams.Persistence;

public static class ExamPersistenceExtensions
{
    public static IServiceCollection AddExamsPersistenceConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["ConnectionStrings:ExamsDatabase"];
        services.AddDbContext<ExamsDbContext>(opt =>
            opt.UseSqlServer(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "exams")));

        return services;
    }
}