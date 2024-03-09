using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.Leave;

public class LeaveAssignmentGroupCommandHandler : ICommandHandler<LeaveAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
    }

    public async Task<Result> Handle(LeaveAssignmentGroupCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
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

        await _context.AssignmentAnswerDegrees
            .Where(x => x.GroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
        
        await _context.AssignmentAnswers
            .Where(x => x.AssignmentGroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);

        await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .ExecuteDeleteAsync(cancellationToken);

        await _fileServices.Delete(answers);
        return Result.Success();
    }
}