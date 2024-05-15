using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.AddSupervisor;

public class AddRoomSupervisorCommandHandler : ICommandHandler<AddRoomSupervisorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public AddRoomSupervisorCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AddRoomSupervisorCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            supervisorId = request.Ids.UserId,
            roomId = request.Ids.RoomId;

        var roomDoctor = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomDoctor is null || roomDoctor != userId)
            return RoomErrors.UnAuthorizeAddSupervisor;

        var userRoom = await _context.UserRooms
            .Where(x => x.RoomId == roomId && x.UserId == supervisorId)
            .FirstOrDefaultAsync(cancellationToken);

        userRoom.IsSupervisor = true;

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new RoomSupervisorAddedEvent(userRoom));

        return Result.Success();
    }
}