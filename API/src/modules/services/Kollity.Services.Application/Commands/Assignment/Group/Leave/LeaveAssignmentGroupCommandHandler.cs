using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.Leave;

public class LeaveAssignmentGroupCommandHandler : ICommandHandler<LeaveAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly IFileServices _fileServices;
    private readonly EventCollection _eventCollection;

    public LeaveAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices,
        IFileServices fileServices, EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _fileServices = fileServices;
        _eventCollection = eventCollection;
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

        var assignmentGroupStudent = await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted)
            .FirstOrDefaultAsync(cancellationToken);
        if (assignmentGroupStudent is null)
            return AssignmentErrors.UserIsNotInTheGroup;
        _context.AssignmentGroupStudents.Remove(assignmentGroupStudent);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new AssignmentGroupStudentLeavedEvent(assignmentGroupStudent));

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