namespace Kollity.Services.Contracts.Room;

public class RoomUserJoinRequestSentIntegrationEvent
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
}