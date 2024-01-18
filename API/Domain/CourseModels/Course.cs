using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.DoctorModels;
using Domain.StudentModels;

namespace Domain.CourseModels;

public class Course
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public List<StudentsCourses> StudentsCourses { get; set; } = [];
    public List<AssignmentGroupsStudents> AssignmentGroupsStudents { get; set; } = [];
}