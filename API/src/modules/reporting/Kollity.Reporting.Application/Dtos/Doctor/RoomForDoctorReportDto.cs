using Kollity.Reporting.Application.Dtos.Course;

namespace Kollity.Reporting.Application.Dtos.Doctor;

public class RoomForDoctorReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public ExamsForCourseReportDto Exams { get; set; }
    public AssignmentsForCourseReportDto Assignments { get; set; }
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDepartment { get; set; }
    public int CourseCode { get; set; }
}