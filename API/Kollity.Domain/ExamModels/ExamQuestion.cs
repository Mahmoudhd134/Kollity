namespace Kollity.Domain.ExamModels;

public class ExamQuestion
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }
    public string Question { get; set; }
<<<<<<< HEAD
    public int OpenForSeconds { get; set; } = 60;
=======
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    public List<ExamQuestionOption> ExamQuestionOptions { get; set; }
    public List<ExamAnswer> ExamAnswers { get; set; } = [];
}