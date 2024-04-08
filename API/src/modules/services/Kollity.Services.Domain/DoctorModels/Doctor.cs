using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.Identity.User;

namespace Kollity.Services.Domain.DoctorModels;

public class Doctor : BaseUser
{
    public List<Course> Courses { get; set; } = [];
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}