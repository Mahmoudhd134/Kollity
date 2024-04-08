namespace Kollity.Services.Contracts.Exam;

public class ExamOptionMarkedAsRightOptionIntegrationEvent
{
    public Guid Id { get; set; }
    public Guid ExamQuestionId { get; set; }
}