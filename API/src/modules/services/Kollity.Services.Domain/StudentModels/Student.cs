using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Domain.Identity.User;

namespace Kollity.Services.Domain.StudentModels;

public class Student : BaseUser
{
    public string Code { get; set; }
    public List<StudentCourse> StudentsCourses { get; set; } = [];
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
    public List<AssignmentGroupStudent> AssignmentGroupsStudents { get; set; } = [];
}