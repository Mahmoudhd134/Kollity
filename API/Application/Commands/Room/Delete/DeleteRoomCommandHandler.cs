using Application.Abstractions;
using Domain.Identity.Role;
using Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Room.Delete;

public class DeleteRoomCommandHandler : ICommandHandler<DeleteRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public DeleteRoomCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == request.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.IdNotFound(request.RoomId);

        var isAdmin = _userAccessor.GetCurrentUserRoles().Contains(Role.Admin);
        var id = _userAccessor.GetCurrentUserId();
        if (isAdmin == false && room.DoctorId != id)
            return RoomErrors.UnAuthorizeDelete;

        _context.Rooms.Remove(room);
        
        var result = await _context.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}