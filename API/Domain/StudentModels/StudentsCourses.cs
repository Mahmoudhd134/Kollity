using Domain.CourseModels;

namespace Domain.StudentModels;

public class StudentsCourses
{
    public Guid Id { get; set; }
    
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}