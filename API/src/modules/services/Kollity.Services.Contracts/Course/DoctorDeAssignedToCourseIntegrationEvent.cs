namespace Kollity.Services.Contracts.Course;

public class DoctorDeAssignedToCourseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}