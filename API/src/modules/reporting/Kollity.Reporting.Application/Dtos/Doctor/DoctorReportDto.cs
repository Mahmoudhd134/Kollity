namespace Kollity.Reporting.Application.Dtos.Doctor;

public class DoctorReportDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string FullName { get; set; }
    public List<CourseMetaForDoctorReportDto> Courses { get; set; }
    public List<RoomForDoctorReportDto> Rooms { get; set; }
}