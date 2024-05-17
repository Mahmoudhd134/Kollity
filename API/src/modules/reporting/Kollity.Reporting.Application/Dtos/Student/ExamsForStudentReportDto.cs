namespace Kollity.Reporting.Application.Dtos.Student;

public class ExamsForStudentReportDto
{
    public int NumberOfExams { get; set; }
    public double AverageExamQuestionCount { get; set; }
    public double AverageDegreeForEachExam { get; set; }
    public double StudentAverageDegree { get; set; }
    public double AverageAverageDegreesForEachStudent { get; set; }
    public double MaxAverageStudentDegrees { get; set; }
    public double MinAverageStudentDegrees { get; set; }
}