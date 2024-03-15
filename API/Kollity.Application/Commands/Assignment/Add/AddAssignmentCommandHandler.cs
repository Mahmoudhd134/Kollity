using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Events;
using Kollity.Contracts.Dto;
using Kollity.Contracts.Events.Assignment;
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

        _eventCollection.Raise(new AssignmentCreatedEvent(new AssignmentCreatedEventDto
        {
            AssignmentName = assignment.Name,
            OpenUntil = assignment.OpenUntilDate,
            CourseName = room.CourseName,
            RoomName = room.RoomName,
            Users = await _context.UserRooms
                .Where(x => x.RoomId == roomId)
                .Where(x => x.User.EmailConfirmed && x.User.EnabledEmailNotifications)
                .Select(x => new UserEmailDto()
                {
                    FullName = x.User.FullNameInArabic,
                    Email = x.User.Email
                })
                .ToListAsync(cancellationToken),
            RoomId = roomId,
            AssignmentId = assignment.Id
        }));

        return Result.Success();
    }
}