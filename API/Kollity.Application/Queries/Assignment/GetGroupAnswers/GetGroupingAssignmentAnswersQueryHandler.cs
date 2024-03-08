using Kollity.Application.Dtos.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetGroupAnswers;

public class GetGroupingAssignmentAnswersQueryHandler :
    IQueryHandler<GetGroupingAssignmentAnswersQuery, GroupingAssignmentAnswersDto>
{
    private readonly ApplicationDbContext _context;

    public GetGroupingAssignmentAnswersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GroupingAssignmentAnswersDto>> Handle(GetGroupingAssignmentAnswersQuery request,
        CancellationToken cancellationToken)
    {
        var assignmentId = request.AssignmentId;
        var filter = request.Filters;

        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => new GroupingAssignmentAnswersDto
            {
                AssignmentId = x.Id,
                AssignmentDegree = x.Degree,
                AssignmentName = x.Name,
                AssignmentMode = x.Mode,
                NumberOfAnswers = x.AssignmentsAnswers.Count(xx => xx.AssignmentGroupId != null),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(assignmentId);

        var codeFilter = filter.GroupCode != null;

        var groups = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId)
            .Select(x => new GroupAssignmentAnswerDto()
            {
                Id = x.AssignmentGroupId ?? Guid.Empty,
                UploadDate = x.UploadDate,
                Code = x.AssignmentGroup.Code,
                AnswerId = x.Id
            })
            .Where(x => codeFilter == false || x.Code.ToString().StartsWith(filter.GroupCode.ToString()))
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        assignmentDto.Groups = groups;
        return assignmentDto;
    }
}