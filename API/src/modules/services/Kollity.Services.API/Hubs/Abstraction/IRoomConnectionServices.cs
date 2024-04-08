namespace Kollity.Services.API.Hubs.Abstraction;

public interface IRoomConnectionServices
{
    public bool AddConnection(string connectionId, Guid userId, Guid roomId);
    public bool RemoveConnection(string connectionId);
    public Guid GetConnectionRoomId(string connectionId);
}