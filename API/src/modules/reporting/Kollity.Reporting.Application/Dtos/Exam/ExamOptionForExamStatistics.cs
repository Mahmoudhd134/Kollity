namespace Kollity.Reporting.Application.Dtos.Exam;

public class ExamOptionForExamStatistics
{
    public Guid Id { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
    public int Count { get; set; }
}