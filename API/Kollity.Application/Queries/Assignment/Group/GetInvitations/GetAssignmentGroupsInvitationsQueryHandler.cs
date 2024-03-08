using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetInvitations;

public class
    GetAssignmentGroupsInvitationsQueryHandler : IQueryHandler<GetAssignmentGroupsInvitationsQuery,
    List<AssignmentGroupInvitationDto>>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetAssignmentGroupsInvitationsQueryHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetAssignmentGroupsInvitationsQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result<List<AssignmentGroupInvitationDto>>> Handle(GetAssignmentGroupsInvitationsQuery request,
        CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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