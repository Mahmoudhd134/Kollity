using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Room;

public class RoomConfig : IEntityTypeConfiguration<Domain.RoomModels.Room>
{
    public void Configure(EntityTypeBuilder<Domain.RoomModels.Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(227).IsRequired();

        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.CourseId);

        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.DoctorId);

        builder.ToTable("Room");
    }
}