namespace Kollity.Services.Contracts.Course;

public class StudentDeAssignedFromCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
}