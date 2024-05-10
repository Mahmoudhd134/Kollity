using Kollity.Reporting.Domain.RoomModels;

namespace Kollity.Reporting.Domain.CourseModels;

public class Course
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public List<Room> Rooms { get; set; } = [];
    public List<CourseDoctorAndAssistants> CourseDoctorAndAssistants { get; set; } = [];
    public List<CourseStudent> Students { get; set; } = [];
}