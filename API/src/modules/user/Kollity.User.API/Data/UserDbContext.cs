using Kollity.User.API.Extensions;
using Kollity.User.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kollity.User.API.Data;

public class UserDbContext : IdentityDbContext<BaseUser, IdentityRole<Guid>, Guid>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("user");
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        builder.ApplyUtcDateTimeConverter();
    }
}