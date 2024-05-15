using Kollity.Services.Application.Dtos.Assignment.Group;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetInvitations;

public class
    GetAssignmentGroupsInvitationsQueryHandler : IQueryHandler<GetAssignmentGroupsInvitationsQuery,
    List<AssignmentGroupInvitationDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetAssignmentGroupsInvitationsQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<List<AssignmentGroupInvitationDto>>> Handle(GetAssignmentGroupsInvitationsQuery request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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