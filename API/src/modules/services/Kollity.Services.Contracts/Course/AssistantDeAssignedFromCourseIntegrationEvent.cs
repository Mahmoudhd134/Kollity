namespace Kollity.Services.Contracts.Course;

public class AssistantDeAssignedFromCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}