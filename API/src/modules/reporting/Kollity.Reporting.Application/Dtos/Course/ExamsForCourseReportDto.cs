namespace Kollity.Reporting.Application.Dtos.Course;

public class ExamsForCourseReportDto
{
    public int NumberOfExams { get; set; }
    public double AverageExamQuestionCount { get; set; }
    public double AverageDegreeForEachExam { get; set; }
    public double AverageAverageDegreesForEachStudent { get; set; }
    public double MaxAverageStudentDegrees { get; set; }
    public double MinAverageStudentDegrees { get; set; }
}