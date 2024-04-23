namespace Kollity.Services.Contracts.Course;

public class StudentDeAssignedFromCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
}