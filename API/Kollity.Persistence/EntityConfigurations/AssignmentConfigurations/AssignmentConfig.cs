using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.AssignmentConfigurations;

public class AssignmentConfig : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(4095).IsRequired();

        builder
            .HasOne(x => x.Room)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.Doctor)
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.Mode)
            .HasConversion<int>();

        builder.ToTable("Assigment");
    }
}