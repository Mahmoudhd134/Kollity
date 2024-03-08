using Kollity.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Persistence.EntityConfigurations.Messages;

public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasMaxLength(1023).IsRequired();
        builder.Property(x => x.Content).IsRequired();

        builder.HasIndex(x => x.ProcessedOn);

        builder.ToTable("OutboxMessage");
    }
}