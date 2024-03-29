﻿using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Assignment.Group.CancelInvitation;

public class
    CancelJoinAssignmentGroupInvitationCommandHandler : ICommandHandler<CancelJoinAssignmentGroupInvitationCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;

    public CancelJoinAssignmentGroupInvitationCommandHandler(ApplicationDbContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
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

        await _context.AssignmentGroupStudents
            .Where(x => x.StudentId == studentId && x.AssignmentGroupId == groupId && x.JoinRequestAccepted == false)
            .ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}