using Kollity.Services.Application.Dtos.Reports;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Reports.UserRoomReport;

public class GetStudentRoomReportQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetStudentRoomReportQuery, StudentRoomReportDto>
{
    public async Task<Result<StudentRoomReportDto>> Handle(GetStudentRoomReportQuery request,
        CancellationToken cancellationToken)
    {
        Guid studentId = request.StudentId,
            roomId = request.RoomId;

        var student = await context.Students
            .Where(x => x.Id == studentId)
            .Select(x => new
            {
                x.Id,
                x.UserName,
                x.Code,
                x.FullNameInArabic,
                x.ProfileImage,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (student is null)
            return StudentErrors.IdNotFound(studentId);

        var isJoined = await context.UserRooms
            .AnyAsync(x => x.UserId == studentId && x.RoomId == roomId && x.JoinRequestAccepted, cancellationToken);
        if (isJoined == false)
            return RoomErrors.UserIsNotJoined(student.UserName);

        var room = await context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => new
            {
                RoomId = x.Id,
                RoomName = x.Name,
                CourseId = x.Course.Id,
                CourseName = x.Course.Name,
                StudentGroupId = x.AssignmentGroups.Where(g =>
                        g.AssignmentGroupsStudents.Any(gs => gs.StudentId == studentId && gs.JoinRequestAccepted))
                    .Select(g => g.Id)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync(cancellationToken);

        var exams = await context.Exams
            .Where(x => x.RoomId == roomId)
            .Select(x => new StudentExamForStudentRoomReportDto
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                NumberOfQuestions = x.ExamQuestions.Count,
                TotalDegree = x.ExamQuestions
                    .Select(q => (int)q.Degree)
                    .Sum(),
                StudentDegree = x.Answers
                    .Where(a => a.StudentId == studentId)
                    .Select(a => a.ExamQuestionOption.IsRightOption ? a.ExamQuestion.Degree : 0)
                    .Sum()
            })
            .ToListAsync(cancellationToken);

        var assignments = await context.Assignments
            .Where(x => x.RoomId == roomId)
            .Select(x => new StudentAssignmentForStudentRoomReportDto
            {
                Id = x.Id,
                Name = x.Name,
                Mode = x.Mode,
                StartDate = x.CreatedDate,
                EndDate = x.OpenUntilDate,
                SubmitDate = x.AssignmentsAnswers
                    .Where(a => a.StudentId == studentId || a.AssignmentGroupId == room.StudentGroupId)
                    .Select(a => a.UploadDate)
                    .FirstOrDefault(),
                StudentDegree = x.AssignmentsAnswers
                    .Where(a => a.StudentId == studentId || a.AssignmentGroupId == room.StudentGroupId)
                    .Select(a => (a.Degree ?? 0) + a.GroupDegrees
                        .Where(gd => gd.GroupId == room.StudentGroupId && gd.StudentId == studentId)
                        .Select(gd => gd.Degree)
                        .FirstOrDefault()
                    )
                    .FirstOrDefault(),
            })
            .ToListAsync(cancellationToken);

        var report = new StudentRoomReportDto
        {
            RoomId = room.RoomId,
            RoomName = room.RoomName,
            CourseId = room.CourseId,
            CourseName = room.CourseName,
            StudentId = student.Id,
            UserName = student.UserName,
            FullName = student.FullNameInArabic,
            Code = student.Code,
            ProfileImage = student.ProfileImage,
            Exams = exams,
            Assignments = assignments
        };

        return report;
    }
}