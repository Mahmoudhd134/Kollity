namespace Kollity.Contracts.Course;

public class AssistantDeAssignedToCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}