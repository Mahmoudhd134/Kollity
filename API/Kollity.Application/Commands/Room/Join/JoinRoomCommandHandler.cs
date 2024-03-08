using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Join;

public class JoinRoomCommandHandler : ICommandHandler<JoinRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public JoinRoomCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
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