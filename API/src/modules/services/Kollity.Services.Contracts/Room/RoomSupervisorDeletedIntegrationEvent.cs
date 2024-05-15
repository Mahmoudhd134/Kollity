namespace Kollity.Services.Contracts.Room;

public class RoomSupervisorDeletedIntegrationEvent
{
    public Guid RoomId { get; set; }
    public Guid SupervisorId { get; set; }
}