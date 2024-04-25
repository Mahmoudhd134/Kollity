using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Add;

public class AddRoomCommandHandler : ICommandHandler<AddRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly IUserServices _userServices;

    public AddRoomCommandHandler(ApplicationDbContext context, EventCollection eventCollection,
        IUserServices userServices)
    {
        _context = context;
        _eventCollection = eventCollection;
        _userServices = userServices;
    }

    public async Task<Result> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        var courseId = request.AddRoomDto.CourseId;
        var course = await _context.Courses
            .Where(x => x.Id == courseId)
            .Select(x => new
            {
                x.DoctorId,
                Assistants = x.CoursesAssistants.Select(a => a.AssistantId)
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (course is null)
            return CourseErrors.IdNotFound(courseId);

        var id = _userServices.GetCurrentUserId();
        if (course.DoctorId != id && course.Assistants.Contains(id) == false)
            return CourseErrors.UnAuthorizeAddRoom;

        var room = new Domain.RoomModels.Room
        {
            Name = request.AddRoomDto.Name,
            CourseId = request.AddRoomDto.CourseId,
            DoctorId = id,
            EnsureJoinRequest = false,
            AssignmentGroupOperationsEnabled = true,
            AssignmentGroupMaxLength = 10
        };
        
        _context.Rooms.Add(room);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new RoomAddedEvent(room));
        return Result.Success();
    }
}