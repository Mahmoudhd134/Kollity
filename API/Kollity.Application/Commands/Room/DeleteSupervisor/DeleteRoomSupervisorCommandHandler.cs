using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.DeleteSupervisor;

public class DeleteRoomSupervisorCommandHandler : ICommandHandler<DeleteRoomSupervisorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public DeleteRoomSupervisorCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result> Handle(DeleteRoomSupervisorCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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