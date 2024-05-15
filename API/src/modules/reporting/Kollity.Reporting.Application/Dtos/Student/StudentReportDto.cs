namespace Kollity.Reporting.Application.Dtos.Student;

public class StudentReportDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string FullName { get; set; }
    public List<CourseMetaForStudentReportDto> Courses { get; set; }
    public List<RoomForStudentReportDto> Rooms { get; set; }
}