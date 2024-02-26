using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.Leave;

public class LeaveAssignmentGroupCommandHandler : ICommandHandler<LeaveAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
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

        var studentCount = await _context.AssignmentGroupStudents
            .CountAsync(x => x.AssignmentGroupId == groupId && x.JoinRequestAccepted, cancellationToken);

        if (studentCount != 0)
            return Result.Success();

        var answers = await _context.AssignmentAnswers
            .Where(x => x.AssignmentGroupId == groupId)
            .Select(x => x.File)
            .ToListAsync(cancellationToken);

        await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .ExecuteDeleteAsync(cancellationToken);

        await _fileAccessor.Delete(answers);
        return Result.Success();
    }
}