using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Events;
using Kollity.Application.Events.Assignment.Created;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Add;

public class AddAssignmentCommandHandler : ICommandHandler<AddAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public AddAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddAssignmentCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(AddAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
<<<<<<< HEAD
            userId = _userAccessor.GetCurrentUserId();
=======
            userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        // var isSupervisor = await _context.UserRooms
        //     .AnyAsync(x => x.RoomId == roomId && x.UserId == userId && x.IsSupervisor, cancellationToken);
        //
        // if (isSupervisor == false)
        //     return AssignmentErrors.UnAuthorizedAdd;

        var roomDoctor = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => x.DoctorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomDoctor is null || roomDoctor == Guid.Empty)
            return AssignmentErrors.RoomHasNoDoctor;

        if (roomDoctor != userId)
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
<<<<<<< HEAD
            OpenUntilDate = request.AddAssignmentDto.OpenUntilDate,
=======
            OpenUntilDate = request.AddAssignmentDto.OpenUntilDate.ToUniversalTime(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            Degree = request.AddAssignmentDto.Degree
        };

        _context.Assignments.Add(assignment);
        var result = await _context.SaveChangesAsync(cancellationToken);

<<<<<<< HEAD
        return result > 0 ? Result.Success() : Error.UnKnown;
=======
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new AssignmentCreatedEvent(assignment.Id));

        return Result.Success();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }
}