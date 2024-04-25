namespace Kollity.Services.Contracts.Course;

public class CourseDeletedIntegrationEvent : BaseIntegrationEvent
{
    public Guid Id { get; set; }
}