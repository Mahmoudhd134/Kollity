using Kollity.Domain.DoctorModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.DoctorConfigurations;

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
                PasswordHash = "AQAAAAIAAYagAAAAEPRFyxksWTOaY3gzYwnqUGS8FT0q1kCjlaUo1KP/Uu3R1seoxDWoi1tlyw8Uc69YNA==",
                SecurityStamp = "6TPMB3KY7R4NAIGXTMKLOWGRE2HQOOBY",
                ConcurrencyStamp = "a443bf96-da75-4046-8452-7d64553b4533",
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        ]);
    }
}