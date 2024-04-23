namespace Kollity.Services.Contracts.Course;

public class DoctorAssignedToCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}