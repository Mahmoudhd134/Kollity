using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetInvitations;

public class
    GetAssignmentGroupsInvitationsQueryHandler : IQueryHandler<GetAssignmentGroupsInvitationsQuery,
    List<AssignmentGroupInvitationDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public GetAssignmentGroupsInvitationsQueryHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result<List<AssignmentGroupInvitationDto>>> Handle(GetAssignmentGroupsInvitationsQuery request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            roomId = request.RoomId;

        var invitations = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .SelectMany(x =>
                x.AssignmentGroupsStudents.Where(s => s.StudentId == userId && s.JoinRequestAccepted == false))
            .Select(x => new AssignmentGroupInvitationDto
            {
                GroupId = x.AssignmentGroupId,
                GroupCode = x.AssignmentGroup.Code
            })
            .ToListAsync(cancellationToken);
        return invitations;
    }
}