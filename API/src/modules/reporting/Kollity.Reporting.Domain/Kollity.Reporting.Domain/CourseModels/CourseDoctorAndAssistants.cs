using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.CourseModels;

public class CourseDoctorAndAssistants
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid DoctorId { get; set; }
    public User Doctor { get; set; }

    public bool IsDoctor { get; set; }
    public DateTime AssigningDate { get; set; }
    public bool IsCurrentlyAssigned { get; set; }
}