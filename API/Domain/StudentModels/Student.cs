using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.Identity.User;

namespace Domain.StudentModels;

public class Student : BaseUser
{
    public string FullNameInArabic { get; set; }
    public List<StudentsCourses> StudentsCourses { get; set; } = [];
    public List<AssignmentGroupsStudents> AssignmentGroupsStudents { get; set; } = [];
}