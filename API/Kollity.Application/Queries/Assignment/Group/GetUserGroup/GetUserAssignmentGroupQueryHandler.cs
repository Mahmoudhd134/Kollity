using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetUserGroup;

public class GetUserAssignmentGroupQueryHandler : IQueryHandler<GetUserAssignmentGroupQuery, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetUserAssignmentGroupQueryHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetUserAssignmentGroupQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result<AssignmentGroupDto>> Handle(GetUserAssignmentGroupQuery request,
        CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
<<<<<<< HEAD
            userId = _userAccessor.GetCurrentUserId();
=======
            userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        var group = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .Where(x =>
                x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId && xx.JoinRequestAccepted))
            .Select(x => new AssignmentGroupDto
            {
                Id = x.Id,
                Code = x.Code,
                Members = x.AssignmentGroupsStudents
                    .Select(xx => new AssignmentGroupMemberDto
                    {
                        Id = xx.StudentId,
                        Code = xx.Student.Code,
                        ProfileImage = xx.Student.ProfileImage,
                        UserName = xx.Student.UserName,
                        FullName = xx.Student.FullNameInArabic,
                        IsJoined = xx.JoinRequestAccepted
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (group is not null) return group;

        var roomName = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(roomName))
            return RoomErrors.NotFound(roomId);

        return AssignmentErrors.UserIsNotInAnyGroupInRoom(roomName);
    }
}