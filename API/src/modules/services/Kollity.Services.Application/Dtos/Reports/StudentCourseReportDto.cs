namespace Kollity.Services.Application.Dtos.Reports;

public class StudentCourseReportDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public string ProfileImage { get; set; }
    private List<StudentRoomForStudentCourseReportDto> Rooms { get; set; }
}