using Kollity.Reporting.Domain.CourseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.Course;

public class CourseDoctorAndAssistantConfig : IEntityTypeConfiguration<CourseDoctorAndAssistants>
{
    public void Configure(EntityTypeBuilder<CourseDoctorAndAssistants> builder)
    {
        builder.HasKey(x => new { x.CourseId, x.DoctorId });

        builder
            .HasOne(x => x.Course)
            .WithMany(x => x.DoctorsAndAssistants)
            .HasForeignKey(x => x.CourseId);
        
        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.DoctorsAndAssistants)
            .HasForeignKey(x => x.DoctorId);

        builder.ToTable("CourseDoctorAndAssistant");
    }
}