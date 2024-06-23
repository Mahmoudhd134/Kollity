namespace Kollity.Exams.Contracts.Exam;

public class ExamAnswerSubmittedIntegrationEvent
{
    public Guid ExamId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserId { get; set; }
    public DateTime RequestTime { get; set; }
    public DateTime SubmitTime { get; set; }
}