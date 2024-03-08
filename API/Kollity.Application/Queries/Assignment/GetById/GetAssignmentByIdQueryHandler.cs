using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetAssignmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetAssignmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result<AssignmentDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == request.AssignmentId)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(request.AssignmentId);

<<<<<<< HEAD
        var isStudent = _userAccessor.IsInRole(Role.Student);
        var userId = _userAccessor.GetCurrentUserId();
=======
        var isStudent = _userServices.IsInRole(Role.Student);
        var userId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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