namespace Kollity.Services.Contracts.Course;

public class DoctorAssignedToCourseIntegrationEvent : BaseIntegrationEvent
{
    public Guid CourseId { get; set; }
    public Guid DoctorId { get; set; }
}