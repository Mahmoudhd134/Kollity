namespace Kollity.Services.Contracts.Exam;

public class ExamQuestionAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
    public Guid ExamId { get; set; }
}