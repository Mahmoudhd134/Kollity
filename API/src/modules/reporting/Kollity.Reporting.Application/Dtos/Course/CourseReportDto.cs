namespace Kollity.Reporting.Application.Dtos.Course;

public class CourseReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public string Department { get; set; }
    public int Hours { get; set; }
    public List<DoctorForCourseReportDto> Doctors { get; set; }
    public List<RoomForCourseReportDto> Rooms { get; set; }
}