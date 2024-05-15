using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Messages.Disconnect;

public class UserDisconnectRoomCommandHandler : ICommandHandler<UserDisconnectRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public UserDisconnectRoomCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(UserDisconnectRoomCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userServices.GetCurrentUserId();
        await _context.UserRooms
            .Where(x => x.UserId == userId && x.RoomId == roomId && x.JoinRequestAccepted)
            .ExecuteUpdateAsync(c =>
                c.SetProperty(x => x.LastOnlineDate, DateTime.UtcNow), cancellationToken);
        return Result.Success();
    }
}