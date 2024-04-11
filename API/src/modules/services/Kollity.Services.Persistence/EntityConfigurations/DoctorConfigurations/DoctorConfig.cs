using Kollity.Services.Domain.DoctorModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.DoctorConfigurations;

public class DoctorConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasData([
            new Doctor
            {
                Id = new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
                UserName = "Mahmoudhd134",
                NormalizedUserName = "MAHMOUDHD134",
                FullNameInArabic = "Mahmoud Ahmed Nasser Mahmoud",
                Email = "nassermahmoud571@gmail.com",
                NormalizedEmail = "NASSERMAHMOUD571@GMAIL.COM",
            }
        ]);
    }
}