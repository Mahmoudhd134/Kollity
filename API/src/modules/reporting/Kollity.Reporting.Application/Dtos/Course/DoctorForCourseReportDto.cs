using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Application.Dtos.Course;

public class DoctorForCourseReportDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Image { get; set; }
    public DateTime AssignedAtUtc { get; set; }
    public DoctorType DoctorType { get; set; }
    public bool IsCurrentlyAssigned { get; set; }
}