namespace Kollity.Services.Contracts.Course;

public class StudentDeAssignedFromCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
}