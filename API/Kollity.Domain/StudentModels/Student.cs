using Kollity.Domain.AssignmentModels;
using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.Identity.User;

namespace Kollity.Domain.StudentModels;

public class Student : BaseUser
{
    public string FullNameInArabic { get; set; }
    public string Code { get; set; }
    public List<StudentCourse> StudentsCourses { get; set; } = [];
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
    public List<AssignmentGroupStudent> AssignmentGroupsStudents { get; set; } = [];
}