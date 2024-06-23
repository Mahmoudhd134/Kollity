using Kollity.Exams.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Exams.Persistence.ExamConfigurations;

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
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("RoomUser");
    }
}