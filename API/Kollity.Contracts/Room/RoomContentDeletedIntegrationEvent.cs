namespace Kollity.Contracts.Room;

public class RoomContentDeletedIntegrationEvent
{
    public Guid RoomId { get; set; }
    public Guid ContentId { get; set; }
}