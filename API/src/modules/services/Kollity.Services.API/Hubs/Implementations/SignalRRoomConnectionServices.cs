using System.Collections.Concurrent;
using Kollity.Services.Application.Abstractions.RealTime;
using Kollity.Services.API.Hubs.Abstraction;

namespace Kollity.Services.API.Hubs.Implementations;

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