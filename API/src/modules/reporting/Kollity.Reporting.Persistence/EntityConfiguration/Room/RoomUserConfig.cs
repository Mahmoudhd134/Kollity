using Kollity.Reporting.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Room;

public class RoomUserConfig : IEntityTypeConfiguration<RoomUser>
{
    public void Configure(EntityTypeBuilder<RoomUser> builder)
    {
        builder.HasKey(x => new { x.RoomId, x.UserId });

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.RoomUsers)
            .HasForeignKey(x => x.RoomId);
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.RoomUsers)
            .HasForeignKey(x => x.UserId);
        
        builder.ToTable("RoomUser");
    }
}