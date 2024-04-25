namespace Kollity.Services.Contracts.Course;

public class DoctorDeAssignedFromCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}