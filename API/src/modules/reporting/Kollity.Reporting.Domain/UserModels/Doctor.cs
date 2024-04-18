using Kollity.Reporting.Domain.CourseModels;

namespace Kollity.Reporting.Domain.UserModels;

public class Doctor : User
{
    public DoctorType DoctorType { get; set; }
    public List<CourseDoctorAndAssistants> DoctorsAndAssistants { get; set; } = [];
}