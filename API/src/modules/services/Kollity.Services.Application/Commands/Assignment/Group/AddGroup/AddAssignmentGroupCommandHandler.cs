﻿using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Dtos.Assignment.Group;
using Kollity.Services.Application.Events.AssignmentGroup;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Assignment.Group.AddGroup;

public class AddAssignmentGroupCommandHandler : ICommandHandler<AddAssignmentGroupCommand, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddAssignmentGroupCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result<AssignmentGroupDto>> Handle(AddAssignmentGroupCommand request,
        CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var userId = _userServices.GetCurrentUserId();
        var ids = request.AddAssignmentGroupDto.Ids.Append(userId).Distinct().ToList();

        var room = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => new
            {
                x.AssignmentGroupOperationsEnabled,
                x.AssignmentGroupMaxLength,
                x.Name,
                CourseName = x.Course.Name
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (room is null)
            return RoomErrors.NotFound(roomId);

        if (room.AssignmentGroupOperationsEnabled == false)
            return AssignmentErrors.OperationIsOff;

        if (ids.Count > room.AssignmentGroupMaxLength)
            return AssignmentErrors.MaxLengthExceeds(room.AssignmentGroupMaxLength);

        //check if all students are in the room 
        var usersInRoom = await _context.UserRooms
            .Where(x => x.RoomId == roomId && ids.Contains(x.UserId))
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken);

        var usersNotInRoom = ids.Except(usersInRoom).ToList();
        if (usersNotInRoom.Count != 0)
        {
            var userNames = await _context.Users
                .Where(x => usersNotInRoom.Contains(x.Id))
                .Select(x => x.UserName)
                .ToListAsync(cancellationToken);
            if (userNames.Count == 0)
                return UserErrors.IdNotFound(usersNotInRoom.First());

            return userNames.Select(RoomErrors.UserIsNotJoined).ToList();
        }

        //check if all students not in another group
        var usersInAnotherGroup = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .SelectMany(x => x.AssignmentGroupsStudents.Where(x => x.JoinRequestAccepted))
            .Select(x => x.StudentId)
            .Where(x => ids.Contains(x))
            .Distinct()
            .ToListAsync(cancellationToken);


        if (usersInAnotherGroup.Count != 0)
        {
            var userNames = await _context.Users
                .Where(x => usersInAnotherGroup.Contains(x.Id))
                .Select(x => x.UserName)
                .ToListAsync(cancellationToken);
            return userNames.Select(AssignmentErrors.UserIsInAnotherGroup).ToList();
        }

        //add the group with students

        var group = new AssignmentGroup
        {
            RoomId = roomId,
            AssignmentGroupsStudents = ids.Select(x => new AssignmentGroupStudent
                {
                    StudentId = x,
                    JoinRequestAccepted = x == userId
                })
                .ToList()
        };
        _context.AssignmentGroups.Add(group);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;


        var members = await _context.Students
            .Where(x => ids.Contains(x.Id))
            .Select(x =>
                new AssignmentGroupMemberDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Code = x.Code,
                    ProfileImage = x.ProfileImage,
                    FullName = x.FullNameInArabic
                })
            .ToListAsync(cancellationToken);
        members.ForEach(x => x.IsJoined = x.Id == userId);

        var dto = new AssignmentGroupDto
        {
            Id = group.Id,
            Code = group.Code,
            RoomId = roomId,
            Members = members
        };
        _eventCollection.Raise(new AssignmentGroupCreatedEvent(dto));
        return dto;
    }
}