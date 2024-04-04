namespace Kollity.Application.Abstractions.RealTime;

public interface IRoomConnectionsServices
{
    public List<Guid> GetUsersConnectedToRoom(Guid roomId);
}