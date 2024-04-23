namespace Kollity.Services.Contracts.Course;

public class StudentAssignedToCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
}