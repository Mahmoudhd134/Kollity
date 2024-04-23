using Kollity.Reporting.Domain.CourseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Course;

public class CourseStudentConfig : IEntityTypeConfiguration<CourseStudent>
{
    public void Configure(EntityTypeBuilder<CourseStudent> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.StudentId);
        
        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.CourseId);
        
        builder.HasIndex(x => new { x.CourseId, x.StudentId });

        builder.ToTable("CourseStudent");
    }
}