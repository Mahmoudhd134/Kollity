namespace Kollity.Services.Contracts.Room;

public class RoomUsersJoinRequestDeniedIntegrationEvent
{
    public Guid Id { get; set; }
    public List<Guid> UserIds { get; set; }
}