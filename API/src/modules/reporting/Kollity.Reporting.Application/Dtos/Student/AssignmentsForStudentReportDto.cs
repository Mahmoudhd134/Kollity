namespace Kollity.Reporting.Application.Dtos.Student;

public class AssignmentsForStudentReportDto
{
    public int NumberOfAssignments { get; set; }
    public double AverageDegreeForEachAssignment { get; set; }
    public double AverageAverageDegreesForEachStudent { get; set; }
    public double MaxAverageStudentDegrees { get; set; }
    public double MinAverageStudentDegrees { get; set; }
    public double AvgerageStudentDegree { get; set; }
}