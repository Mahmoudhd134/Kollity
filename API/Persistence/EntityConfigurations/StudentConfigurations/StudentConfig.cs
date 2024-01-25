using Domain.StudentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.StudentConfigurations;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.FullNameInArabic).HasMaxLength(127);
        builder.Property(x => x.Code).HasMaxLength(15).IsRequired();
    }
}