using Kollity.Application.Abstractions;
using Kollity.Domain.AssignmentModels;
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
        
        var roomOperationsState = await _context.Rooms
            .Where(x => x.AssignmentGroups.Any(xx => xx.Id == groupId))
            .Select(x => x.AssignmentGroupOperationsEnabled)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomOperationsState == false)
            return AssignmentErrors.OperationIsOff;

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted)
            .ExecuteDeleteAsync(cancellationToken);
        return Result.Success();
    }
}