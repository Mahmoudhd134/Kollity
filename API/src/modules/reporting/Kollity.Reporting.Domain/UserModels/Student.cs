using Kollity.Reporting.Domain.AssignmentModels;
using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Domain.ExamModels;

namespace Kollity.Reporting.Domain.UserModels;

public class Student : User
{
    public string Code { get; set; }

    public List<AssignmentAnswer> AssignmentAnswers { get; set; } = [];
    public List<CourseStudent> Courses { get; set; } = [];
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}