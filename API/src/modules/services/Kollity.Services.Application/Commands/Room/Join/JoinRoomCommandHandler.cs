using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Join;

public class JoinRoomCommandHandler : ICommandHandler<JoinRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public JoinRoomCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            roomId = request.RoomId;
        var isJoined = await _context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == roomId, cancellationToken);
        if (isJoined)
            return RoomErrors.UserAlreadyJoinedTheRoom;

        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound(roomId);

        var userRoom = new UserRoom
        {
            UserId = userId,
            LastOnlineDate = DateTime.UtcNow,
            IsSupervisor = false,
            JoinRequestAccepted = room.EnsureJoinRequest == false
        };

        room.UsersRooms.Add(userRoom);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        if (room.EnsureJoinRequest)
            _eventCollection.Raise(new UserJoinRequestSentEvent(userRoom));
        else
            _eventCollection.Raise(new UsersJoinRequestAcceptedEvent([userId], roomId));
        return Result.Success();
    }
}