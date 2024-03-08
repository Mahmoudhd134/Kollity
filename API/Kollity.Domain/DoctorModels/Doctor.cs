using Kollity.Domain.CourseModels;
using Kollity.Domain.Identity.User;

namespace Kollity.Domain.DoctorModels;

public class Doctor : BaseUser
{
    public List<Course> Courses { get; set; } = [];
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}