using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.AcceptAllJoins;

public class AcceptAllRoomJoinRequestsCommandHandler : ICommandHandler<AcceptAllRoomJoinRequestsCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public AcceptAllRoomJoinRequestsCommandHandler(ApplicationDbContext context, EventCollection eventCollection,
        IUserServices userServices)
    {
        _context = context;
        _eventCollection = eventCollection;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AcceptAllRoomJoinRequestsCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            currentUserId = _userServices.GetCurrentUserId();

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x => x.UserId == currentUserId && x.RoomId == roomId && x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAcceptJoinRequest;

        var ids = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.JoinRequestAccepted == false)
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken);

        await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.JoinRequestAccepted == false)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);

        _eventCollection.Raise(new UsersJoinRequestAcceptedEvent(ids, roomId));

        return Result.Success();
    }
}