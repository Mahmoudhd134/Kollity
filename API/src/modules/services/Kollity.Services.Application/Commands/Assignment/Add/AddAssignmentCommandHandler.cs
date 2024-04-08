using Kollity.Services.Application.Abstractions;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Events.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Add;

public class AddAssignmentCommandHandler : ICommandHandler<AddAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AddAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userServices.GetCurrentUserId();

        // var isSupervisor = await _context.UserRooms
        //     .AnyAsync(x => x.RoomId == roomId && x.UserId == userId && x.IsSupervisor, cancellationToken);
        //
        // if (isSupervisor == false)
        //     return AssignmentErrors.UnAuthorizedAdd;

        var room = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => new
            {
                x.DoctorId,
                RoomName = x.Name,
                CourseName = x.Course.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
            return RoomErrors.NotFound(roomId);
        if (room.DoctorId is null)
            return AssignmentErrors.RoomHasNoDoctor;
        if (room.DoctorId != userId)
            return AssignmentErrors.UnAuthorizedAdd;


        var assignment = new Domain.AssignmentModels.Assignment
        {
            RoomId = roomId,
            DoctorId = userId,
            Name = request.AddAssignmentDto.Name,
            Description = request.AddAssignmentDto.Description,
            Mode = request.AddAssignmentDto.Mode,
            CreatedDate = DateTime.UtcNow,
            LastUpdateDate = DateTime.UtcNow,
            OpenUntilDate = request.AddAssignmentDto.OpenUntilDate.ToUniversalTime(),
            Degree = request.AddAssignmentDto.Degree
        };

        _context.Assignments.Add(assignment);
        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new AssignmentCreatedEvent(assignment));

        return Result.Success();
    }
}