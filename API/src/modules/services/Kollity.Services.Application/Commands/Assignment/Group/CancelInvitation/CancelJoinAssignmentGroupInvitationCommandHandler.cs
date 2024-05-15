using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.CancelInvitation;

public class
    CancelJoinAssignmentGroupInvitationCommandHandler : ICommandHandler<CancelJoinAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public CancelJoinAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(CancelJoinAssignmentGroupInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        //check user is in the group
        var isJoined = await _context.AssignmentGroupStudents
            .AnyAsync(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted,
                cancellationToken);
        if (isJoined == false)
            return AssignmentErrors.UserIsNotInTheGroup;

        var assignmentGroupStudent = await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == studentId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted == false)
            .FirstOrDefaultAsync(cancellationToken);
        if (assignmentGroupStudent is null)
            return AssignmentErrors.UserIsNotInTheGroup;

        _context.AssignmentGroupStudents.Remove(assignmentGroupStudent);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new AssignmentGroupInvitationCanceledEvent(assignmentGroupStudent));
        return Result.Success();
    }
}