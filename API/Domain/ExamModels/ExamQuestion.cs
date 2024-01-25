namespace Domain.ExamModels;

public class ExamQuestion
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; } = 60;
    public List<ExamQuestionOption> ExamQuestionOptions { get; set; }
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}