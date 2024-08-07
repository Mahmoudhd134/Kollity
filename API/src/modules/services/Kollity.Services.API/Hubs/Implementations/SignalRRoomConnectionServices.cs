﻿using System.Collections.Concurrent;
using Kollity.Services.API.Hubs.Abstraction;
using Kollity.Services.Application.Abstractions.RealTime;

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
        if (found == false)
            return Guid.Empty;
        return userRoom.RoomId;
    }

    public List<string> GetUserRoomConnectionId(Guid userId, Guid roomId)
    {
        return _roomConnections
            .Where(x => x.Value.RoomId == roomId && x.Value.UserId == userId)
            .Select(x => x.Key)
            .ToList();
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