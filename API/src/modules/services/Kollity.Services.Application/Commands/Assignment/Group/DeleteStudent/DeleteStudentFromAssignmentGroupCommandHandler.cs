using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.DeleteStudent;

public class DeleteStudentFromAssignmentGroupCommandHandler : ICommandHandler<DeleteStudentFromAssignmentGroupCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeleteStudentFromAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeleteStudentFromAssignmentGroupCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        var roomOperationsState = await _context.Rooms
            .Where(x => x.AssignmentGroups.Any(xx => xx.Id == groupId))
            .Select(x => x.AssignmentGroupOperationsEnabled)
            .FirstOrDefaultAsync(cancellationToken);

        if (roomOperationsState == false)
            return AssignmentErrors.OperationIsOff;


        var assignmentGroupStudent = await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == studentId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted)
            .FirstOrDefaultAsync(cancellationToken);
        if (assignmentGroupStudent is null)
            return AssignmentErrors.UserIsNotInTheGroup;

        _context.AssignmentGroupStudents.Remove(assignmentGroupStudent);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new AssignmentGroupStudentDeletedEvent(assignmentGroupStudent));
        return Result.Success();
    }
}