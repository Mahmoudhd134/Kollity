using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.DeleteSupervisor;

public class DeleteRoomSupervisorCommandHandler : ICommandHandler<DeleteRoomSupervisorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteRoomSupervisorCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
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

        var userRoom = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == supervisorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (userRoom is null)
            return RoomErrors.UserIsNotJoined(supervisorId);

        userRoom.IsSupervisor = false;

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new RoomSupervisorDeletedEvent(userRoom));

        return Result.Success();
    }
}