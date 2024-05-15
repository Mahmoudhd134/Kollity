using Kollity.Services.Domain.DoctorModels;
using Kollity.Services.Domain.RoomModels;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Domain.CourseModels;

public class Course
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }

    public Guid? DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public List<Room> Rooms { get; set; } = [];
    public List<StudentCourse> StudentsCourses { get; set; } = [];
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}