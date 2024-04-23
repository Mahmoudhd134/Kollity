namespace Kollity.Services.Contracts.Course;

public class AssistantAssignedToCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}