namespace Kollity.Services.Contracts.Course;

public class CourseDeletedIntegrationEvent : GenericIntegrationEvent
{
    public Guid Id { get; set; }
}