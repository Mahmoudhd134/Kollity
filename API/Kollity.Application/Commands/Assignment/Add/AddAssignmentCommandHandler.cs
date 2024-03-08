using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Events;
using Kollity.Application.Events.Assignment.Created;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Add;

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
            OpenUntilDate = request.AddAssignmentDto.OpenUntilDate.ToUniversalTime(),
            Degree = request.AddAssignmentDto.Degree
        };

        _context.Assignments.Add(assignment);
        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new AssignmentCreatedEvent(assignment.Id));

        return Result.Success();
    }
}