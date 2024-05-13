namespace Kollity.Reporting.Application.Queries.Common;

public class ExamQueryResult
{
    public int NumberOfExams { get; set; }
    public int CountOfAllQuestions { get; set; }
    public double? AvgQuestionDegree { get; set; }
    public int? SumOfAllDegrees { get; set; }
    public int? MaxSumOfStudentDegree { get; set; }
    public int? MinSumOfStudentDegree { get; set; }
    public int? NumberOfStudentsAnswers { get; set; }
}