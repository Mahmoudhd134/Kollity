namespace Kollity.Services.Contracts.Room;

public class RoomUsersJoinedIntegrationEvent
{
    public Guid Id { get; set; }
    public List<Guid> UserIds { get; set; }
}