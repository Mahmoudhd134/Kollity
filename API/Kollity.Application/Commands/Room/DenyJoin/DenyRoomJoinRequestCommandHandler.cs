using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DenyJoin;

public class DenyRoomJoinRequestCommandHandler : ICommandHandler<DenyRoomJoinRequestCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DenyRoomJoinRequestCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DenyRoomJoinRequestCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.Ids.RoomId,
            userId = request.Ids.UserId,
            currentUserId = _userServices.GetCurrentUserId();

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