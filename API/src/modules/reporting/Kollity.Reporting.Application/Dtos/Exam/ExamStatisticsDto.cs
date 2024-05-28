namespace Kollity.Reporting.Application.Dtos.Exam;

public class ExamStatisticsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }

    public int NumberOfAnswers { get; set; }
    public int MaxDegree { get; set; }
    public int MinDegree { get; set; }
    public double AverageDegree { get; set; }
    public List<ExamQuestionForExamStatistics> Questions { get; set; }
}

public class ExamQuestionForExamStatistics
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int Degree { get; set; }
    public int OpenForSeconds { get; set; }
    public List<ExamOptionForExamStatistics> Options { get; set; }
}

public class ExamOptionForExamStatistics
{
    public Guid Id { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
    public int Count { get; set; }
}