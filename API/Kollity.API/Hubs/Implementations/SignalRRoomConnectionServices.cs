using System.Collections.Concurrent;
using Kollity.API.Hubs.Abstraction;
using Kollity.Application.Abstractions.RealTime;

namespace Kollity.API.Hubs.Implementations;

public class SignalRRoomConnectionServices : IRoomConnectionsServices, IRoomConnectionServices
{
    private readonly ConcurrentDictionary<string, UserRoom> _roomConnections = [];

    public bool AddConnection(string connectionId, Guid userId, Guid roomId)
    {
        return _roomConnections.TryAdd(connectionId, new UserRoom
        {
            UserId = userId,
            RoomId = roomId
        });
    }

    public bool RemoveConnection(string connectionId)
    {
        return _roomConnections.TryRemove(connectionId, out _);
    }

    public Guid GetConnectionRoomId(string connectionId)
    {
        var found = _roomConnections.TryGetValue(connectionId, out var userRoom);
        if(found == false)
            return Guid.Empty;
        return userRoom.RoomId;
    }

    public List<Guid> GetUsersConnectedToRoom(Guid roomId)
    {
        return _roomConnections
            .Where(kvp => kvp.Value.RoomId == roomId)
            .Select(kvp => kvp.Value.UserId)
            .Distinct()
            .ToList();
    }
}