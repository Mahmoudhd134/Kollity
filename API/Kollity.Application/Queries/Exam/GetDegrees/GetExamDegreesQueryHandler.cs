using Kollity.Application.Dtos.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Exam.GetDegrees;

public class GetExamDegreesQueryHandler : IQueryHandler<GetExamDegreesQuery, ExamDegreesDto>
{
    private readonly ApplicationDbContext _context;

    public GetExamDegreesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ExamDegreesDto>> Handle(GetExamDegreesQuery request, CancellationToken cancellationToken)
    {
        var examId = request.ExamId;

        var fullNameFilter = string.IsNullOrWhiteSpace(request.Filters.FullName) == false;
        var codeFilter = string.IsNullOrWhiteSpace(request.Filters.Code) == false;

        var examDto = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(x => new
            {
                RoomId = x.RoomId,
                RoomDoctorId = x.Room.DoctorId,
                ExamDto = new ExamDegreesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ExamTotalDegree = x.ExamQuestions.Sum(xx => xx.Degree),
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (examDto is null)
            return ExamErrors.IdNotFound(examId);

        List<UserForExamDegreesDto> users = [];
        examDto.ExamDto.Users = users;
        if (request.Filters.WhoSolve)
        {
            users.AddRange(await _context.ExamAnswers
                .Where(x => x.ExamId == examId)
                .Select(x => new
                {
                    x.StudentId,
                    x.Student.FullNameInArabic,
                    x.Student.Code,
                    x.ExamQuestionOption.IsRightOption,
                    x.ExamQuestion.Degree
                })
                .GroupBy(x => x.StudentId)
                .Select(studentAnswersGroup => new UserForExamDegreesDto
                {
                    Id = studentAnswersGroup.Key ?? Guid.Empty,
                    FullName = studentAnswersGroup.First().FullNameInArabic,
                    Code = studentAnswersGroup.First().Code,
                    Degree = studentAnswersGroup.Sum(a =>
                        a.IsRightOption ? a.Degree : 0)
                })
                .Where(x => fullNameFilter == false || x.FullName.StartsWith(request.Filters.FullName))
                .Where(x => codeFilter == false || x.Code.StartsWith(request.Filters.Code))
                .Skip(request.Filters.PageSize * request.Filters.PageIndex)
                .Take(request.Filters.PageSize)
                .ToListAsync(cancellationToken));
        }

        var solversId = await _context.ExamAnswers
            .Where(x => x.ExamId == examId)
            .Where(x => x.StudentId != null)
            .Select(x => (Guid)x.StudentId)
            .Distinct()
            .ToListAsync(cancellationToken);

        examDto.ExamDto.CountOfUsersSolve = solversId.Count;

        if (request.Filters.WhoDidNotSolve == false)
            return examDto.ExamDto;

        if (examDto.ExamDto.Users.Count == request.Filters.PageSize)
            return examDto.ExamDto;


        var ps = request.Filters.PageSize;
        var needed = ps - users.Count;
        int newPageIndex;
        int newPageSize;
        int offset;

        if (request.Filters.WhoSolve == false)
        {
            newPageIndex = request.Filters.PageIndex;
            newPageSize = request.Filters.PageSize;
            offset = 0;
        }
        else
        {
            var reminder = solversId.Count % ps;
            if (reminder == 0)
            {
                var pages = solversId.Count / ps;
                newPageIndex = request.Filters.PageIndex - pages;
                newPageSize = ps;
                offset = 0;
            }
            else
            {
                if (needed < ps)
                {
                    newPageIndex = 0;
                    newPageSize = needed;
                    offset = 0;
                }
                else
                {
                    offset = ps - reminder;
                    newPageIndex = request.Filters.PageIndex - solversId.Count / ps - 1;
                    newPageSize = ps;
                }
            }
        }

        var unSolvers = await _context.Students
            .Where(s => solversId.Contains(s.Id) == false &&
                        s.UsersRooms.Any(xx => xx.RoomId == examDto.RoomId))
            .Select(x => new UserForExamDegreesDto
            {
                Id = x.Id,
                Code = x.Code,
                FullName = x.FullNameInArabic
            })
            .Skip(newPageIndex * ps + offset)
            .Take(newPageSize)
            .ToListAsync(cancellationToken);

        users.AddRange(unSolvers);

        return examDto.ExamDto;
    }
}