using Kollity.Services.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollity.Services.Persistence.EntityConfigurations.RoomConfigurations;

public class MessagePollAnswerConfig : IEntityTypeConfiguration<MessagePollAnswer>
{
    public void Configure(EntityTypeBuilder<MessagePollAnswer> builder)
    {
        builder.HasKey(x => new { x.PollId, x.UserId });

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Poll)
            .WithMany(x => x.PollAnswers)
            .HasForeignKey(x => x.PollId);

        builder.HasIndex(x => x.OptionIndex);

        builder.ToTable("MessagePollAnswer");
    }
}