using Kollity.Services.Application.Abstractions;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.GetIndividualAnswers;

public class
    GetAssignmentIndividualAnswersQueryHandler : IQueryHandler<GetAssignmentIndividualAnswersQuery,
    IndividualAssignmentAnswersDto>
{
    private readonly ApplicationDbContext _context;

    public GetAssignmentIndividualAnswersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IndividualAssignmentAnswersDto>> Handle(GetAssignmentIndividualAnswersQuery request,
        CancellationToken cancellationToken)
    {
        var assignmentId = request.AssignmentId;

        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == assignmentId)
            .Select(x => new IndividualAssignmentAnswersDto
            {
                AssignmentId = x.Id,
                AssignmentDegree = x.Degree,
                AssignmentName = x.Name,
                AssignmentMode = x.Mode,
                NumberOfAnswers = x.AssignmentsAnswers.Count(xx => xx.StudentId != null),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(assignmentId);

        var filters = request.Filters;
        var filterStudentCode = string.IsNullOrWhiteSpace(filters.StudentCode) == false;
        var filterStudentName = string.IsNullOrWhiteSpace(filters.StudentFullName) == false;

        var students = await _context.AssignmentAnswers
            .Where(x => x.AssignmentId == assignmentId)
            .Where(x => filterStudentCode == false || x.Student.Code.StartsWith(filters.StudentCode))
            .Where(x => filterStudentName == false || x.Student.FullNameInArabic.StartsWith(filters.StudentFullName))
            .Select(x => new StudentAssignmentAnswerDto
            {
                Id = x.StudentId ?? Guid.Empty,
                Degree = x.Degree,
                FullName = x.Student.FullNameInArabic,
                UserName = x.Student.UserName,
                ProfileImage = x.Student.ProfileImage,
                Code = x.Student.Code,
                UploadDate = x.UploadDate,
                AnswerId = x.Id
            })
            .Skip(filters.PageIndex * filters.PageSize)
            .Take(filters.PageSize)
            .ToListAsync(cancellationToken);

        assignmentDto.Students = students;
        return assignmentDto;
    }
}