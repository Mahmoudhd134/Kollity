using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.AcceptJoin;

public class AcceptRoomJoinRequestCommandHandler : ICommandHandler<AcceptRoomJoinRequestCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public AcceptRoomJoinRequestCommandHandler(ApplicationDbContext context, EventCollection eventCollection,
        IUserServices userServices)
    {
        _context = context;
        _eventCollection = eventCollection;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AcceptRoomJoinRequestCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.Ids.RoomId,
            userId = request.Ids.UserId,
            currentUserId = _userServices.GetCurrentUserId();

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x => x.UserId == currentUserId && x.RoomId == roomId && x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAcceptJoinRequest;

        var result = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == userId)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new UsersJoinRequestAcceptedEvent([userId], roomId));
        return Result.Success();
    }
}