namespace Kollity.Services.Contracts.Course;

public class AssistantAssignedToCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}