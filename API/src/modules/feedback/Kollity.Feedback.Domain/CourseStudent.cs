namespace Kollity.Feedback.Domain;

public class CourseStudent
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; }
}