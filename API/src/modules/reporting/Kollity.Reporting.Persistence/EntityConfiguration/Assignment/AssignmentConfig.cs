using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Assignment;

public class AssignmentConfig : IEntityTypeConfiguration<Domain.AssignmentModels.Assignment>
{
    public void Configure(EntityTypeBuilder<Domain.AssignmentModels.Assignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(4095).IsRequired();
        builder.Property(x => x.Degree).IsRequired().HasDefaultValue(20);

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId);

        builder.Property(x => x.Mode)
            .HasConversion<int>();

        builder.ToTable("Assignment");
    }
}