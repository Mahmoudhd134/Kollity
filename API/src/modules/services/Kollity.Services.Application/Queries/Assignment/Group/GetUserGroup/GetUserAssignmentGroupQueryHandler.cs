﻿using Kollity.Services.Application.Dtos.Assignment.Group;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetUserGroup;

public class GetUserAssignmentGroupQueryHandler : IQueryHandler<GetUserAssignmentGroupQuery, AssignmentGroupDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public GetUserAssignmentGroupQueryHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    public async Task<Result<AssignmentGroupDto>> Handle(GetUserAssignmentGroupQuery request,
        CancellationToken cancellationToken)
    {
        Guid roomId = request.RoomId,
            userId = _userServices.GetCurrentUserId();

        var group = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .Where(x =>
                x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId && xx.JoinRequestAccepted))
            .Select(x => new AssignmentGroupDto
            {
                Id = x.Id,
                Code = x.Code,
                RoomId = x.RoomId,
                Members = x.AssignmentGroupsStudents
                    .Select(xx => new AssignmentGroupMemberDto
                    {
                        Id = xx.StudentId,
                        Code = xx.Student.Code,
                        ProfileImage = xx.Student.ProfileImage,
                        UserName = xx.Student.UserName,
                        FullName = xx.Student.FullNameInArabic,
                        IsJoined = xx.JoinRequestAccepted
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (group is not null) return group;

        var roomName = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(roomName))
            return RoomErrors.NotFound(roomId);

        return AssignmentErrors.UserIsNotInAnyGroupInRoom(roomName);
    }
}