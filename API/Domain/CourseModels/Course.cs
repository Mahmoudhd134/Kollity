using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.DoctorModels;
using Domain.RoomModels;
using Domain.StudentModels;

namespace Domain.CourseModels;

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