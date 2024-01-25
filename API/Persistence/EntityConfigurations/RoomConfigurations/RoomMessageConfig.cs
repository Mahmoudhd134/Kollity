using Domain.Identity.User;
using Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.CourseConfigurations;

namespace Persistence.EntityConfigurations.RoomConfigurations;

public class RoomMessageConfig : IEntityTypeConfiguration<RoomMessage>
{
    public void Configure(EntityTypeBuilder<RoomMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).HasMaxLength(4067);
        builder.Property(x => x.File).HasMaxLength(511);

        builder
            .HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.RoomMessages)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("RoomMessage");
    }
}