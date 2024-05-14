namespace Kollity.Reporting.Application.Dtos.Student;

public class RoomForStudentReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public ExamsForStudentReportDto Exams { get; set; }
    public AssignmentsForStudentReportDto Assignments { get; set; }
}