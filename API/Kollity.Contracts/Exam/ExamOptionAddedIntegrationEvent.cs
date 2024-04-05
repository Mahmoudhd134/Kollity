namespace Kollity.Contracts.Exam;

public class ExamOptionAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Option { get; set; }
    public bool IsRightOption { get; set; }
    public Guid ExamQuestionId { get; set; }
}