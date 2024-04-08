using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.GetGroupDegree;

public class
    GetAssignmentGroupMembersAnswerDegreeQueryHandler : IQueryHandler<GetAssignmentGroupMembersAnswerDegreeQuery,
    AssignmentGroupDegreeDto>
{
    private readonly ApplicationDbContext _context;

    public GetAssignmentGroupMembersAnswerDegreeQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AssignmentGroupDegreeDto>> Handle(GetAssignmentGroupMembersAnswerDegreeQuery request,
        CancellationToken cancellationToken)
    {
        Guid assignmentId = request.AssignmentId,
            groupId = request.GroupId;

        var group = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId && x.AssignmentGroupId == groupId)
            .Select(x => new AssignmentGroupDegreeDto()
            {
                Id = x.AssignmentId,
                Code = x.AssignmentGroup.Code,
                AnswerId = x.Id,
                Members = x.AssignmentGroup.AssignmentGroupsStudents
                    .Select(xx => new AssignmentGroupMemberDegreeDto()
                    {
                        Id = xx.StudentId,
                        FullName = xx.Student.FullNameInArabic,
                        Code = xx.Student.Code
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (group is null)
            return AssignmentErrors.AnswerNotFound;

        var ids = group.Members.Select(x => x.Id).ToList();
        var degrees = await _context.AssignmentAnswerDegrees
            .Where(x => x.AssignmentId == assignmentId && ids.Contains(x.StudentId))
            .Select(x => new
            {
                x.StudentId,
                x.Degree
            })
            .ToDictionaryAsync(x => x.StudentId, cancellationToken);

        group.Members.ForEach(x => x.Degree = degrees.TryGetValue(x.Id, out var degree) ? degree.Degree : null);
        return group;
    }
}