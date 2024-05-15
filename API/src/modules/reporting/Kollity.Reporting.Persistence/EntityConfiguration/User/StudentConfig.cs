using Kollity.Reporting.Domain.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.User;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Code).HasMaxLength(10);
        builder.ToTable("User");
    }
}