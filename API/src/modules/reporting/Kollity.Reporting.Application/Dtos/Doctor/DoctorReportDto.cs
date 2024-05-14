using Kollity.Reporting.Application.Dtos.Course;

namespace Kollity.Reporting.Application.Dtos.Doctor;

public class DoctorReportDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string FullName { get; set; }
    public List<CourseMetaForDoctorReportDto> Courses { get; set; }
    public List<RoomForDoctorReportDto> Rooms { get; set; }
}

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