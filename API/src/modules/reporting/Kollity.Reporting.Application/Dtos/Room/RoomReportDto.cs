using Kollity.Reporting.Application.Dtos.User;
using Kollity.Reporting.Domain.AssignmentModels;

namespace Kollity.Reporting.Application.Dtos.Room;

public class RoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public int MembersCount { get; set; }
    public UserUsernameDto RoomDoctor { get; set; }

    public Guid CourseId { get; set; }
    public string CourseName { get; set; }

    public List<ExamForRoomReportDto> Exams { get; set; }
    public List<AssignmentForRoomReportDto> Assignments { get; set; }
    public List<RoomContentForRoomReportDto> Contents { get; set; }
}

public class ExamForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public int NumberOfQuestions { get; set; }
    public int TotalDegree { get; set; }
    public int NumberOfAnswers { get; set; }
    public int? MaxStudentDegree { get; set; }
    public int? MinStudentDegree { get; set; }
    public double? AvgStudentDegree { get; set; }
}

public class AssignmentForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ReportingAssignmentMode Mode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime OpenTo { get; set; }
    public int Degree { get; set; }
    public int NumberOfAnswers { get; set; }
    public int? MaxStudentDegree { get; set; }
    public int? MinStudentDegree { get; set; }
    public double? AvgStudentDegree { get; set; }
}

public class RoomContentForRoomReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public UserUsernameDto Uploader { get; set; }
    public DateTime UploadedAt { get; set; }
}