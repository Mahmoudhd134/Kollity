using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.AcceptJoin;

public class AcceptRoomJoinRequestCommandHandler : ICommandHandler<AcceptRoomJoinRequestCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public AcceptRoomJoinRequestCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public AcceptRoomJoinRequestCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AcceptRoomJoinRequestCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.Ids.RoomId,
            userId = request.Ids.UserId,
<<<<<<< HEAD
            currentUserId = _userAccessor.GetCurrentUserId();
=======
            currentUserId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x => x.UserId == currentUserId && x.RoomId == roomId && x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAcceptJoinRequest;

        var result = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == userId)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);
        return result == 1 ? Result.Success() : Error.UnKnown;
    }
}