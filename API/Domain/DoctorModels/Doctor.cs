using Domain.CourseModels;
using Domain.Identity.User;

namespace Domain.DoctorModels;

public class Doctor : BaseUser
{
    public List<Course> Courses { get; set; } = [];
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}