namespace Kollity.Reporting.Application.Dtos.Course;

public class RoomForCourseReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public ExamsForCourseReportDto Exams { get; set; }
    public AssignmentsForCourseReportDto Assignments { get; set; }
}