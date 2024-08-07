﻿using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.SendInvitation;

public class SendAssignmentGroupJoinInvitationCommandHandler : ICommandHandler<SendAssignmentGroupJoinInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public SendAssignmentGroupJoinInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(SendAssignmentGroupJoinInvitationCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            groupId = request.InvitationDto.GroupId,
            studentId = request.InvitationDto.StudentId;

        var room = await _context.Rooms
            .Where(x => x.AssignmentGroups.Any(xx => xx.Id == groupId))
            .Select(x => new
            {
                Room = new
                {
                    x.Id,
                    x.AssignmentGroupMaxLength,
                    x.AssignmentGroupOperationsEnabled,
                    x.Name
                },
                Course = new
                {
                    x.Course.Name
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
            return AssignmentErrors.GroupNotFound(groupId);
        if (room.Room.AssignmentGroupOperationsEnabled == false)
            return AssignmentErrors.OperationIsOff;

        //check if the max length dose not exceeds
        var studentCount = await _context.AssignmentGroupStudents
            .CountAsync(x => x.AssignmentGroupId == groupId, cancellationToken);
        if (studentCount >= room.Room.AssignmentGroupMaxLength)
            return AssignmentErrors.MaxLengthExceeds(room.Room.AssignmentGroupMaxLength);

        //check user is in the group
        var users = await _context.AssignmentGroupStudents
            .Where(x => (x.StudentId == userId || x.StudentId == studentId) && x.AssignmentGroupId == groupId)
            .Select(x => new { x.StudentId, x.JoinRequestAccepted })
            .ToDictionaryAsync(x => x.StudentId, cancellationToken);
        if (users[userId].JoinRequestAccepted == false)
            return AssignmentErrors.UserIsNotInTheGroup;

        //check student is not waiting
        if (users.TryGetValue(studentId, out var user))
            return user.JoinRequestAccepted
                ? AssignmentErrors.StudentIsInThisGroup
                : AssignmentErrors.StudentIsWaitingOnThisGroup;


        //check student is in the room
        var isInTheRoom = await _context.UserRooms
            .AnyAsync(x => x.RoomId == room.Room.Id && x.UserId == studentId && x.JoinRequestAccepted,
                cancellationToken);
        if (isInTheRoom == false)
            return RoomErrors.UserIsNotJoined(studentId);

        //check that the student is in no other group within the room
        var isStudentJoinAnotherTeam = await _context.AssignmentGroups
            .Where(x => x.RoomId == room.Room.Id)
            .AnyAsync(x =>
                x.AssignmentGroupsStudents.Any(xx =>
                    xx.StudentId == studentId && xx.JoinRequestAccepted), cancellationToken);
        if (isStudentJoinAnotherTeam)
            return AssignmentErrors.StudentIsInAnotherGroup;

        //add the invitation
        var invitation = new AssignmentGroupStudent
        {
            StudentId = studentId,
            AssignmentGroupId = groupId,
            JoinRequestAccepted = false
        };
        _context.AssignmentGroupStudents.Add(invitation);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new AssignmentGroupInvitationSentEvent(invitation));
        return Result.Success();
    }
}