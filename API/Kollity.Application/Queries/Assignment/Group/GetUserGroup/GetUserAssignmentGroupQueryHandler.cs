using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetUserGroup;

public class GetUserAssignmentGroupQueryHandler : IQueryHandler<GetUserAssignmentGroupQuery, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public GetUserAssignmentGroupQueryHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result<AssignmentGroupDto>> Handle(GetUserAssignmentGroupQuery request,
        CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userAccessor.GetCurrentUserId();

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