namespace Kollity.Services.Contracts.Course;

public class StudentAssignedToCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
}