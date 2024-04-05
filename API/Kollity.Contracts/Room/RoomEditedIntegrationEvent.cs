namespace Kollity.Contracts.Room;

public class RoomEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool EnsureJoinRequest { get; set; }
}