using Kollity.Application.Abstractions;
using Kollity.Domain.AssignmentModels;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.SendInvitation;

public class SendAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<SendAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserAccessor _userAccessor;

    public SendAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(SendAssignmentGroupJoinInvitationCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        //check user is in the group
        var isJoined = await _context.AssignmentGroupStudents
            .AnyAsync(x => x.StudentId == userId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted,
                cancellationToken);
        if (isJoined == false)
            return AssignmentErrors.UserIsNotInTheGroup;
        
        var roomId = await _context.AssignmentGroups
            .Where(x => x.Id == groupId)
            .Select(x => x.RoomId)
            .FirstOrDefaultAsync(cancellationToken);
        
        //check student is in the room
        var isInTheRoom = await _context.UserRooms
            .AnyAsync(x => x.RoomId == roomId && x.UserId == studentId && x.JoinRequestAccepted, cancellationToken);
        if (isInTheRoom == false)
            return RoomErrors.UserIsNotJoined(studentId);
        
        //check that the student is in no other group within the room

        if (roomId == Guid.Empty)
            return AssignmentErrors.GroupNotFound(groupId);

        var isStudentJoinAnotherTeam = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .AnyAsync(x =>
                x.AssignmentGroupsStudents.Any(xx =>
                    xx.StudentId == studentId && xx.JoinRequestAccepted), cancellationToken);
        if (isStudentJoinAnotherTeam)
            return AssignmentErrors.StudentIsInAnotherGroup;
        
        //check that the student is not waiting on this group
        var isInThisGroup = await _context.AssignmentGroupStudents
            .AnyAsync(x => x.StudentId == studentId && x.AssignmentGroupId == groupId, cancellationToken);
        if (isInThisGroup)
            return AssignmentErrors.StudentIsWaitingOnThisGroup;
        
        //add the invitation
        var invitation = new AssignmentGroupStudent()
        {
            StudentId = studentId,
            AssignmentGroupId = groupId,
            JoinRequestAccepted = false
        };
        _context.AssignmentGroupStudents.Add(invitation);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? Result.Success() : Error.UnKnown;
    }
}