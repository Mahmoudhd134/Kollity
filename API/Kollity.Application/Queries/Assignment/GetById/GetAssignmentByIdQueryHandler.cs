using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Dtos.Assignment;
using Kollity.Contracts.Assignment;
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
    private readonly IUserServices _userServices;

    public GetAssignmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
    }

    public async Task<Result<AssignmentDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == request.AssignmentId)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(request.AssignmentId);

        var isStudent = _userServices.IsInRole(Role.Student);
        var userId = _userServices.GetCurrentUserId();
        if (isStudent == false)
            return assignmentDto;

        AnswerDto answerDto;
        if (assignmentDto.Mode == AssignmentMode.Individual)
        {
            answerDto = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == request.AssignmentId && x.StudentId == userId)
                .Select(x => new AnswerDto()
                {
                    Id = x.Id,
                    SolveDate = x.UploadDate,
                    Degree = x.Degree
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (answerDto == default)
                return assignmentDto;

            assignmentDto.IsSolved = true;
            assignmentDto.Answer = answerDto;
            return assignmentDto;
        }

        var groupId = await _context.AssignmentGroups
            .Where(x => x.RoomId == assignmentDto.RoomId)
            .Where(x => x.AssignmentGroupsStudents.Any(xx => xx.StudentId == userId && xx.JoinRequestAccepted))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (groupId == default)
            return assignmentDto;

        answerDto = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == request.AssignmentId && x.AssignmentGroupId == groupId)
            .Select(x => new AnswerDto()
            {
                Id = x.Id,
                SolveDate = x.UploadDate,
                Degree = x.GroupDegrees.FirstOrDefault(xx => xx.StudentId == userId).Degree 
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (answerDto == default)
            return assignmentDto;
        assignmentDto.IsSolved = true;
        assignmentDto.Answer = answerDto;
        return assignmentDto;
    }
}