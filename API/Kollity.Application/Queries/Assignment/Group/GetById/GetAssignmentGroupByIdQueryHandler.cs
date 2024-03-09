using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Group.GetById;

public class GetAssignmentGroupByIdQueryHandler : IQueryHandler<GetAssignmentGroupByIdQuery, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetAssignmentGroupByIdQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<AssignmentGroupDto>> Handle(GetAssignmentGroupByIdQuery request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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