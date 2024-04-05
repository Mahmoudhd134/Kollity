using Kollity.Application.Dtos.Assignment;
using Kollity.Contracts.Assignment;
using Kollity.Domain.AssignmentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.Report;

public class GetAssignmentReportQueryHandler : IQueryHandler<GetAssignmentReportQuery, AssignmentReportDto>
{
    private readonly ApplicationDbContext _context;

    public GetAssignmentReportQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AssignmentReportDto>> Handle(GetAssignmentReportQuery request,
        CancellationToken cancellationToken)
    {
        var id = request.Id;
        int pageIndex = request.Pagination.PageIndex,
            pageSize = request.Pagination.PageSize;

        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == id)
            .Select(x => new AssignmentReportDto()
            {
                Id = x.Id,
                Degree = x.Degree,
                Mode = x.Mode,
                Name = x.Name,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto == null)
            return AssignmentErrors.NotFound(id);


        if (assignmentDto.Mode == AssignmentMode.Individual)
        {
            assignmentDto.CountOfAnswers = await _context.AssignmentAnswers
                .CountAsync(x => x.AssignmentId == id && x.StudentId != null, cancellationToken);

            assignmentDto.Students = await _context.AssignmentAnswers
                .Where(x => x.AssignmentId == id)
                .Select(x => new StudentForAssignmentReportDto()
                {
                    Id = x.Id,
                    Code = x.Student.Code,
                    FullName = x.Student.FullNameInArabic,
                    Degree = x.Degree
                })
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return assignmentDto;
        }

        assignmentDto.CountOfAnswers = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == id && x.AssignmentGroupId != null)
            .SelectMany(x => x.AssignmentGroup.AssignmentGroupsStudents)
            .CountAsync(cancellationToken);
        
        var students = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == id)
            .SelectMany(x => x.AssignmentGroup.AssignmentGroupsStudents)
            .Select(x => new StudentForAssignmentReportDto()
            {
                Id = x.StudentId,
                Code = x.Student.Code,
                FullName = x.Student.FullNameInArabic
            })
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var sIds = students.Select(x => x.Id).ToList();
        var degrees = await _context.AssignmentAnswerDegrees
            .Where(x => x.AssignmentId == id && sIds.Contains(x.StudentId))
            .Select(x => new
            {
                x.StudentId,
                x.Degree
            })
            .ToDictionaryAsync(x => x.StudentId, cancellationToken);
        students.ForEach(x => x.Degree = degrees.TryGetValue(x.Id, out var degree) ? degree.Degree : null);

        assignmentDto.Students = students;
        return assignmentDto;
    }
}