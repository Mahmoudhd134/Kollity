using Application.Abstractions;
using Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Room.DeleteSupervisor;

public class DeleteRoomSupervisorCommandHandler : ICommandHandler<DeleteRoomSupervisorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public DeleteRoomSupervisorCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(DeleteRoomSupervisorCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            supervisorId = request.Ids.UserId,
            roomId = request.Ids.RoomId;

        var roomDoctor = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomDoctor is null || roomDoctor != userId)
            return RoomErrors.UnAuthorizeDeleteSupervisor;

        if (supervisorId == roomDoctor)
            return RoomErrors.DoctorMustBeAnSupervisor;

        await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == supervisorId)
            .ExecuteUpdateAsync(calls => calls
                .SetProperty(x => x.IsSupervisor, false), cancellationToken);

        return Result.Success();
    }
}