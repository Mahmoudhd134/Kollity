namespace Kollity.Services.Contracts.Course;

public class DoctorDeAssignedFromCourseIntegrationEvent : GenericIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}