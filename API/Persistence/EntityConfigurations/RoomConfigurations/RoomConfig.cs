using Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.AssignmentConfigurations.AssignmentGroupModels;
using Persistence.EntityConfigurations.CourseConfigurations;

namespace Persistence.EntityConfigurations.RoomConfigurations;

public class RoomConfig : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(227).IsRequired();
        builder.Property(x => x.Image).HasMaxLength(511).IsRequired();

        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.CourseId);

        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable("Room");
    }
}