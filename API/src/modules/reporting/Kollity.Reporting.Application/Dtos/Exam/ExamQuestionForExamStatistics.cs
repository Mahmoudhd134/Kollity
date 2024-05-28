namespace Kollity.Reporting.Application.Dtos.Exam;

public class ExamQuestionForExamStatistics
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int Degree { get; set; }
    public int OpenForSeconds { get; set; }
    public List<ExamOptionForExamStatistics> Options { get; set; }
}