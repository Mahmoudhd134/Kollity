namespace Kollity.Services.Application.Dtos.Reports;

public class StudentRoomReportDto
{
    public Guid StudentId { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public string ProfileImage { get; set; }

    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public List<StudentExamForStudentRoomReportDto> Exams { get; set; }
    public List<StudentAssignmentForStudentRoomReportDto> Assignments { get; set; }
    
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
}