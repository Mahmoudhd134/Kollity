using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Course;
public class CourseConfig : IEntityTypeConfiguration<Domain.CourseModels.Course>
{
    public void Configure(EntityTypeBuilder<Domain.CourseModels.Course> builder)
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
