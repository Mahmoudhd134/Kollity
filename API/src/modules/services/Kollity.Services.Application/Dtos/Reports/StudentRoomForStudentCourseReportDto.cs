namespace Kollity.Services.Application.Dtos.Reports;

public class StudentRoomForStudentCourseReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StudentExamForStudentRoomReportDto> Exams { get; set; }
    public List<StudentAssignmentForStudentRoomReportDto> Assignments { get; set; }
}