using Kollity.Application.Abstractions;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Add;

public class AddAssignmentCommandHandler : ICommandHandler<AddAssignmentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public AddAssignmentCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(AddAssignmentCommand request, CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userAccessor.GetCurrentUserId();

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


        var assignment = new Domain.AssignmentModels.Assignment()
        {
            RoomId = roomId,
            DoctorId = userId,
            Name = request.AddAssignmentDto.Name,
            Description = request.AddAssignmentDto.Description,
            Mode = request.AddAssignmentDto.Mode,
            CreatedDate = DateTime.UtcNow,
            LastUpdateDate = DateTime.UtcNow
        };

        _context.Assignments.Add(assignment);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}