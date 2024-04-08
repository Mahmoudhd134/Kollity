namespace Kollity.Services.Contracts.Room;

public class RoomJoinRequestsAcceptedIntegrationEvent
{
    public Guid Id { get; set; }
    public List<Guid> UserIds { get; set; }
}