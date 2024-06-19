using Kollity.Feedback.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Feedback.Persistence.Configurations;

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