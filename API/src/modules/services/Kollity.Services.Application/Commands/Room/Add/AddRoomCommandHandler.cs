using Kollity.Services.Application.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Domain.Identity.User;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Add;

public class AddRoomCommandHandler : ICommandHandler<AddRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly UserManager<BaseUser> _userManager;

    public AddRoomCommandHandler(ApplicationDbContext context, IUserServices userServices,
        UserManager<BaseUser> userManager)
    {
        _context = context;
        _userServices = userServices;
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