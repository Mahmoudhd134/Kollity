using Domain.AssignmentModels;
using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.Identity.User;

namespace Domain.StudentModels;

public class Student : BaseUser
{
    public string FullNameInArabic { get; set; }
    public string Code { get; set; }
    public List<StudentCourse> StudentsCourses { get; set; } = [];
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
    public List<AssignmentGroupStudent> AssignmentGroupsStudents { get; set; } = [];
}