namespace Kollity.Reporting.Application.Dtos.Student;

public class CourseMetaForStudentReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public string Department { get; set; }
    public DateTime AssignedDate { get; set; }
    public bool IsCurrentlyAssigned { get; set; }
}