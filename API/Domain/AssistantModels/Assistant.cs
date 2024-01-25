using Domain.CourseModels;
using Domain.Identity.User;

namespace Domain.AssistantModels;

public class Assistant : BaseUser
{
    public List<CourseAssistant> CoursesAssistants { get; set; } = [];
}