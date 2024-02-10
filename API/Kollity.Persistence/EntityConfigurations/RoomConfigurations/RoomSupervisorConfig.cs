namespace Kollity.Persistence.EntityConfigurations.RoomConfigurations;

// public class RoomSupervisorConfig : IEntityTypeConfiguration<RoomSupervisor>
// {
//     public void Configure(EntityTypeBuilder<RoomSupervisor> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder
//             .HasOne(x => x.Room)
//             .WithMany(x => x.RoomsSupervisors)
//             .HasForeignKey(x => x.RoomId);
//
//
//         builder
//             .HasOne(x => x.Supervisor)
//             .WithMany(x => x.RoomsSupervisors)
//             .HasForeignKey(x => x.SupervisorId)
//             .OnDelete(DeleteBehavior.NoAction);
//
//         builder.HasIndex(x => new { x.RoomId, x.SupervisorId }).IsUnique();
//
//         builder.ToTable("RoomSupervisor");
//     }
// }