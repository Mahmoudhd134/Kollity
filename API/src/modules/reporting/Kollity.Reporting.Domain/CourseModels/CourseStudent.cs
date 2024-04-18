using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.CourseModels;

public class CourseStudent
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
}