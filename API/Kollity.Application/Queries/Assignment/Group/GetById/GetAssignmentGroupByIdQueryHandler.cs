using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetById;

public class GetAssignmentGroupByIdQueryHandler : IQueryHandler<GetAssignmentGroupByIdQuery, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetAssignmentGroupByIdQueryHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetAssignmentGroupByIdQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result<AssignmentGroupDto>> Handle(GetAssignmentGroupByIdQuery request,
        CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
            groupId = request.GroupId;

        var group = await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .Select(x => new AssignmentGroupDto
            {
                Id = x.Id,
                Code = x.Code,
                Members = x.AssignmentGroupsStudents
                    .Select(s => new AssignmentGroupMemberDto
                    {
                        Id = s.StudentId,
                        Code = s.Student.Code,
                        ProfileImage = s.Student.ProfileImage,
                        UserName = s.Student.UserName,
                        FullName = s.Student.FullNameInArabic,
                        IsJoined = s.JoinRequestAccepted
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (group.Members.Any(x => x.Id == userId && x.IsJoined) == false)
            group.Members = group.Members.Where(x => x.IsJoined).ToList();

        return group;
    }
}