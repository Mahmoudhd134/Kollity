using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.AcceptAllJoins;

public class AcceptAllRoomJoinRequestsCommandHandler : ICommandHandler<AcceptAllRoomJoinRequestsCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public AcceptAllRoomJoinRequestsCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
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

        await _context.UserRooms
            .Where(x => x.RoomId == roomId)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.JoinRequestAccepted, true), cancellationToken);

        return Result.Success();
    }
}