using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Join;

public class JoinRoomCommandHandler : ICommandHandler<JoinRoomCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public JoinRoomCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public JoinRoomCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            roomId = request.RoomId;
        var isJoined = await _context.UserRooms
            .AnyAsync(x => x.UserId == userId && x.RoomId == roomId, cancellationToken);
        if (isJoined)
            return RoomErrors.UserAlreadyJoinedTheRoom;

        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound(roomId);

        room.UsersRooms.Add(new UserRoom
        {
            UserId = userId,
            LastOnlineDate = DateTime.UtcNow,
            IsSupervisor = false,
            JoinRequestAccepted = room.EnsureJoinRequest == false
        });

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}