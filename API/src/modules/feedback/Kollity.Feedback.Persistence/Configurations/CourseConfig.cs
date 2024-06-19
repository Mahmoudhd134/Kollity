using Kollity.Feedback.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Feedback.Persistence.Configurations;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Department).HasMaxLength(15).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(511).IsRequired();
        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Hours).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();

        builder.ToTable("Course");
    }
}