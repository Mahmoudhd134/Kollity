using Kollity.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.DeclineInvitation;

public class DeclineAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<DeclineAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public DeclineAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(DeclineAssignmentGroupJoinInvitationCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            groupId = request.GroupId;

        await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}