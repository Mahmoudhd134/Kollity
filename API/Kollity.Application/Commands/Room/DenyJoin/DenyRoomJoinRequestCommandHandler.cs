using Kollity.Application.Abstractions;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DenyJoin;

public class DenyRoomJoinRequestCommandHandler : ICommandHandler<DenyRoomJoinRequestCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public DenyRoomJoinRequestCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(DenyRoomJoinRequestCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.Ids.RoomId,
            userId = request.Ids.UserId,
            currentUserId = _userAccessor.GetCurrentUserId();

        var isSupervisor = await _context.UserRooms
            .AnyAsync(x => x.UserId == currentUserId && x.RoomId == roomId && x.IsSupervisor == true,
                cancellationToken);
        if (isSupervisor == false)
            return RoomErrors.UnAuthorizeDenyJoinRequest;

        var result = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}