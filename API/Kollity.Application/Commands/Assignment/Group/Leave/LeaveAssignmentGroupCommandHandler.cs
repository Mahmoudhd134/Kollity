using Kollity.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.Leave;

public class LeaveAssignmentGroupCommandHandler : ICommandHandler<LeaveAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(LeaveAssignmentGroupCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            groupId = request.GroupId;

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted)
            .ExecuteDeleteAsync(cancellationToken);
        return Result.Success();
    }
}