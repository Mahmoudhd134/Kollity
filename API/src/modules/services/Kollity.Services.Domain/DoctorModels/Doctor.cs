using Kollity.Services.Domain.CourseModels;
using Kollity.Services.Domain.Identity;

namespace Kollity.Services.Domain.DoctorModels;

public class Doctor : BaseUser
{
    public List<Course> Courses { get; set; } = [];
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}