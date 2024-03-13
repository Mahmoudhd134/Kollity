using Kollity.Domain.StudentModels;

namespace Kollity.Domain.ExamModels;

public class ExamAnswer
{
    public Guid Id { get; set; }
    public Guid? StudentId { get; set; }
    public Student Student { get; set; }
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }
    public Guid ExamQuestionId { get; set; }
    public ExamQuestion ExamQuestion { get; set; }
    public Guid? ExamQuestionOptionId { get; set; }
    public ExamQuestionOption ExamQuestionOption { get; set; }

    public DateTime RequestTime { get; set; }
    public DateTime? SubmitTime { get; set; }
}