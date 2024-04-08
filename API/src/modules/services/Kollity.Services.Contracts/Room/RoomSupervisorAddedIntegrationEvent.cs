namespace Kollity.Services.Contracts.Room;

public class RoomSupervisorAddedIntegrationEvent
{
    public Guid RoomId { get; set; }
    public Guid SupervisorId { get; set; }
}