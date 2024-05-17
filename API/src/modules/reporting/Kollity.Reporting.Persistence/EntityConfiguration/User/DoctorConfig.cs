using Kollity.Reporting.Domain.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Reporting.Persistence.EntityConfiguration.User;

public class DoctorConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(x => x.DoctorType)
            .HasConversion<int>();
        builder.HasData([
            new Doctor
            {
                Id = new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
                UserName = "Mahmoudhd134",
                FullNameInArabic = "Mahmoud Ahmed Nasser Mahmoud",
                Email = "nassermahmoud571@gmail.com",
                ProfileImage = null,
                IsDeleted = false,
                DoctorType = DoctorType.Doctor,
            }
        ]);
        builder.ToTable("User");
    }
}