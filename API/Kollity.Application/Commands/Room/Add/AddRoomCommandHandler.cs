using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.User;
using Kollity.Domain.RoomModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Add;

public class AddRoomCommandHandler : ICommandHandler<AddRoomCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly UserManager<BaseUser> _userManager;

    public AddRoomCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        UserManager<BaseUser> userManager)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public AddRoomCommandHandler(ApplicationDbContext context, IUserServices userServices,
        UserManager<BaseUser> userManager)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        _userManager = userManager;
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

<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
        room.UsersRooms.Add(new UserRoom
        {
            UserId = id,
            JoinRequestAccepted = true,
            LastOnlineDate = DateTime.UtcNow,
            IsSupervisor = true
        });

        _context.Rooms.Add(room);

        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}