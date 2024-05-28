using Kollity.Common.ErrorHandling;
using Kollity.Reporting.Application.Dtos.Assignment;
using Kollity.Reporting.Application.Dtos.Common;
using Kollity.Reporting.Domain.Errors;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.Queries.Assignment;

public class GetAssignmentStatisticsQueryHandler(ReportingDbContext context)
    : IQueryHandler<GetAssignmentStatisticsQuery, AssignmentStatisticsDto>
{
    public async Task<Result<AssignmentStatisticsDto>> Handle(GetAssignmentStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var assignmentDto = await context.Assignments
            .Where(x => x.Id == request.Id)
            .Select(a => new AssignmentStatisticsDto
            {
                Id = a.Id,
                Name = a.Name,
                Mode = a.Mode,
                CreatedAt = a.CreatedDate,
                Degree = a.Degree,
                OpenTo = a.OpenUntilDate,
                NumberOfAnswers = a.AssignmentsAnswers.Count,
                AvgStudentDegree = a.AssignmentsAnswers.Select(x => x.Degree).Average(),
                MaxStudentDegree = a.AssignmentsAnswers.Select(x => x.Degree).Max(),
                MinStudentDegree = a.AssignmentsAnswers.Select(x => x.Degree).Min(),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(request.Id);

        assignmentDto.Degrees = await context.AssignmentAnswers
            .Where(a => a.AssignmentId == request.Id)
            .GroupBy(a => a.Degree)
            .Select(g => new DegreeCount
            {
                Degree = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);


        return assignmentDto;
    }
}