namespace Kollity.Services.Contracts.Course;

public class AssistantDeAssignedFromCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid AssistantId { get; set; }
}