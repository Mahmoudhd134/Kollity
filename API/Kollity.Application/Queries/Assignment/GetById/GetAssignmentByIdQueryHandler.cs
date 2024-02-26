﻿using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Assignment;
using Kollity.Domain.AssignmentModels;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.Role;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetById;

public class GetAssignmentByIdQueryHandler : IQueryHandler<GetAssignmentByIdQuery, AssignmentDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetAssignmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result<AssignmentDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == request.AssignmentId)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(request.AssignmentId);

        var isStudent = _userAccessor.IsInRole(Role.Student);
        var userId = _userAccessor.GetCurrentUserId();
        if (isStudent == false)
            return assignmentDto;

        DateTime answeredDate;
        if (assignmentDto.Mode == AssignmentMode.Individual)
        {
            answeredDate = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == request.AssignmentId && x.StudentId == userId)
                .Select(x => x.UploadDate)
                .FirstOrDefaultAsync(cancellationToken);
            if (answeredDate == default)
                return assignmentDto;

            assignmentDto.IsSolved = true;
            assignmentDto.SolveDate = answeredDate;
            return assignmentDto;
        }

        var groupId = await _context.AssignmentGroups
            .Where(x => x.RoomId == assignmentDto.RoomId)
            .Where(x => x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId && xx.JoinRequestAccepted))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (groupId == default)
            return assignmentDto;

        answeredDate = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == request.AssignmentId && x.AssignmentGroupId == groupId)
            .Select(x => x.UploadDate)
            .FirstOrDefaultAsync(cancellationToken);
        if (answeredDate == default)
            return assignmentDto;
        assignmentDto.IsSolved = true;
        assignmentDto.SolveDate = answeredDate;
        return assignmentDto;
    }
}