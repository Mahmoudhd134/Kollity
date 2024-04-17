using Kollity.Reporting.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Persistence.Data;

public class ReportingDbContext : DbContext
{
    public ReportingDbContext(DbContextOptions<ReportingDbContext> options) : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("services");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceExtensions).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}