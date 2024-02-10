using Kollity.Application.Abstractions;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.AcceptAllJoins;

public class AcceptAllRoomJoinRequestsCommandHandler : ICommandHandler<AcceptAllRoomJoinRequestsCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public AcceptAllRoomJoinRequestsCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(AcceptAllRoomJoinRequestsCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            currentUserId = _userAccessor.GetCurrentUserId();

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x => x.UserId == currentUserId && x.RoomId == roomId && x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeAcceptJoinRequest;

        await _context.UserRooms
            .Where(x => x.RoomId == roomId)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);

        return Result.Success();
    }
}