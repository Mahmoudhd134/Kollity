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

    public async Task<Result> Handle(SendAssignmentGroupJoinInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userAccessor.GetCurrentUserId(),
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        var room = await _context.Rooms
            .Where(x => x.AssignmentGroups.Any(xx => xx.Id == groupId))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        if (room is null)
            return AssignmentErrors.GroupNotFound(groupId);
        if (room.AssignmentGroupOperationsEnabled == false)
            return AssignmentErrors.OperationIsOff;

        //check if the max length dose not exceeds
        var studentCount = await _context.AssignmentGroupStudents
            .CountAsync(x => x.AssignmentGroupId == groupId, cancellationToken);
        if (studentCount >= room.AssignmentGroupMaxLength)
            return AssignmentErrors.MaxLengthExceeds(room.AssignmentGroupMaxLength);

        //check user is in the group
        var users = await _context.AssignmentGroupStudents
            .Where(x => (x.StudentId == userId || x.StudentId == studentId) && x.AssignmentGroupId == groupId)
            .Select(x => new { x.StudentId, x.JoinRequestAccepted })
            .ToDictionaryAsync(x => x.StudentId, cancellationToken);
        if (users[userId].JoinRequestAccepted == false)
            return AssignmentErrors.UserIsNotInTheGroup;
        
        //check student is not waiting
        if (users.TryGetValue(studentId, out var user))
            return user.JoinRequestAccepted ? AssignmentErrors.StudentIsInThisGroup: AssignmentErrors.StudentIsWaitingOnThisGroup;


        //check student is in the room
        var isInTheRoom = await _context.UserRooms
            .AnyAsync(x => x.RoomId == room.Id && x.UserId == studentId && x.JoinRequestAccepted, cancellationToken);
        if (isInTheRoom == false)
            return RoomErrors.UserIsNotJoined(studentId);

        //check that the student is in no other group within the room
        var isStudentJoinAnotherTeam = await _context.AssignmentGroups
            .Where(x => x.RoomId == room.Id)
            .AnyAsync(x =>
                x.AssignmentGroupsStudents.Any(xx =>
                    xx.StudentId == studentId && xx.JoinRequestAccepted), cancellationToken);
        if (isStudentJoinAnotherTeam)
            return AssignmentErrors.StudentIsInAnotherGroup;

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