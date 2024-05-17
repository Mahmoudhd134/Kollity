using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.DeclineInvitation;

public class
    DeclineAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<DeclineAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public DeclineAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(DeclineAssignmentGroupJoinInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.GroupId;

        var assignmentGroupStudent = await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId && x.JoinRequestAccepted == false)
            .FirstOrDefaultAsync(cancellationToken);
        if (assignmentGroupStudent is null)
            return AssignmentErrors.UserIsNotInTheGroup;

        _context.AssignmentGroupStudents.Remove(assignmentGroupStudent);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new AssignmentGroupInvitationDeclinedEvent(assignmentGroupStudent));
        return Result.Success();
    }
}