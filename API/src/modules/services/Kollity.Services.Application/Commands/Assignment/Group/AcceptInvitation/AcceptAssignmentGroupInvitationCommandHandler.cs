using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.AcceptInvitation;

public class AcceptAssignmentGroupInvitationCommandHandler : ICommandHandler<AcceptAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AcceptAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AcceptAssignmentGroupInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.GroupId;

        var assignmentGroupStudent = await _context.AssignmentGroupStudents
            .Where(x => x.AssignmentGroupId == groupId && x.StudentId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (assignmentGroupStudent is null)
            return AssignmentErrors.GroupNotFound(groupId);

        var roomId = await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .Select(x => x.RoomId)
            .FirstOrDefaultAsync(cancellationToken);
        var groupsId = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var isJoined = await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == userId &&
                        groupsId.Contains(x.AssignmentGroupId) &&
                        x.JoinRequestAccepted)
            .AnyAsync(cancellationToken);
        if (isJoined)
            return AssignmentErrors.StudentIsInAnotherGroup;

        assignmentGroupStudent.JoinRequestAccepted = true;
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new AssignmentGroupInvitationAcceptedEvent(assignmentGroupStudent));
        return Result.Success();
    }
}