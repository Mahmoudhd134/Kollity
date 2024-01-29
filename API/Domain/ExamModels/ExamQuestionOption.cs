namespace Domain.ExamModels;

public class ExamQuestionOption
{
    public Guid Id { get; set; }
    public Guid ExamQuestionId { get; set; }
    public ExamQuestion ExamQuestion { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}