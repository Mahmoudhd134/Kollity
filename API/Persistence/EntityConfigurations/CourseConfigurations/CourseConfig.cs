using Domain.CourseModels;
using Domain.DoctorModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityConfigurations.AssignmentConfigurations.AssignmentGroupModels;
using Persistence.EntityConfigurations.StudentConfigurations;

namespace Persistence.EntityConfigurations.CourseConfigurations;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Department).HasMaxLength(15).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(511).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Hours).IsRequired();

        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.DoctorId)
            .IsRequired(false);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.ToTable("Course");
    }
}