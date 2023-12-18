using Domain.Doctor;
using Domain.Identity;
using Domain.Student;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence.Data;

public class ApplicationDbContext : IdentityDbContext<BaseUser, BaseRole, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PersistenceExtensions).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}