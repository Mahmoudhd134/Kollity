namespace Kollity.Services.Contracts.Course;

public class CourseAddedIntegrationEvent : GenericIntegrationEvent
{
    public Guid Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }
}