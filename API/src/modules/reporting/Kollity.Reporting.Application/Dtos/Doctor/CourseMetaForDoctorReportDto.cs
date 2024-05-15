using Kollity.Reporting.Application.Dtos.Course;

namespace Kollity.Reporting.Application.Dtos.Doctor;

public class CourseMetaForDoctorReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public DateTime AssignedDate { get; set; }
    public bool IsDoctor { get; set; }
    public string Department { get; set; }
    public bool IsCurrentlyAssigned { get; set; }
}