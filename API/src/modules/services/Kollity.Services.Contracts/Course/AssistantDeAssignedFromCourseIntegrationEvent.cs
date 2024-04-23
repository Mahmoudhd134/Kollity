namespace Kollity.Services.Contracts.Course;

public class AssistantDeAssignedFromCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}