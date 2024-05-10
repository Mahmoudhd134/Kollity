namespace Kollity.Reporting.Domain.ExamModels;

public class ExamQuestion
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
    public List<ExamQuestionOption> ExamQuestionOptions { get; set; } = [];
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}