using Kollity.Common.ErrorHandling;
using Kollity.Reporting.Application.Dtos.Room;
using Kollity.Reporting.Application.Dtos.User;
using Kollity.Reporting.Domain.Errors;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Reporting.Application.Queries.Room;

public class RoomReportQueryHandler(ReportingDbContext context) : IQueryHandler<RoomReportQuery, RoomReportDto>
{
    public async Task<Result<RoomReportDto>> Handle(RoomReportQuery request, CancellationToken cancellationToken)
    {
        var roomDto = await context.Rooms
            .Where(x => x.Id == request.Id)
            .Select(r => new RoomReportDto
            {
                Id = r.Id,
                Name = r.Name,
                CreatedAt = r.CreatedAt,
                MembersCount = r.RoomUsers.Count,
                RoomDoctor = new UserUsernameDto
                {
                    Id = r.DoctorId,
                    Image = r.Doctor.ProfileImage,
                    UserName = r.Doctor.UserName
                },
                CourseId = r.CourseId,
                CourseName = r.Course.Name,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (roomDto is null)
            return RoomErrors.NotFound(request.Id);

        var exams = await context.Exams
            .Where(x => x.RoomId == request.Id)
            .Select(e => new ExamForRoomReportDto
            {
                Id = e.Id,
                Name = e.Name,
                CreatedAt = e.CreationDate,
                StartsAt = e.StartDate,
                EndsAt = e.EndDate,
                TotalDegree = e.ExamQuestions.Select(q => (int)q.Degree).Sum(),
                NumberOfQuestions = e.ExamQuestions.Count,
                NumberOfAnswers = e.Answers.Select(a => a.StudentId).Distinct().Count(),
                AvgStudentDegree = e.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Average(),
                MaxStudentDegree = e.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Max(),
                MinStudentDegree = e.Answers
                    .GroupBy(a => a.StudentId)
                    .Select(g => g.Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0).Sum())
                    .Min()
            })
            .ToListAsync(cancellationToken);

        var assignments = await context.Assignments
            .Where(x => x.RoomId == request.Id)
            .Select(a => new AssignmentForRoomReportDto
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
            .ToListAsync(cancellationToken);

        roomDto.Exams = exams;
        roomDto.Assignments = assignments;

        return roomDto;
    }
}