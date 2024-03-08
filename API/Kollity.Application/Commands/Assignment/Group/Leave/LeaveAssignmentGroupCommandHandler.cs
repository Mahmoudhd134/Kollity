using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Files;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.Leave;

public class LeaveAssignmentGroupCommandHandler : ICommandHandler<LeaveAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
    private readonly IFileAccessor _fileAccessor;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor,
        IFileAccessor fileAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _fileAccessor = fileAccessor;
=======
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(LeaveAssignmentGroupCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        Guid userId = _userAccessor.GetCurrentUserId(),
=======
        Guid userId = _userServices.GetCurrentUserId(),
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
=======
        
        await _context.AssignmentAnswers
            .Where(x => x.AssignmentGroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

        await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .ExecuteDeleteAsync(cancellationToken);

<<<<<<< HEAD
        await _fileAccessor.Delete(answers);
=======
        await _fileServices.Delete(answers);
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        return Result.Success();
    }
}