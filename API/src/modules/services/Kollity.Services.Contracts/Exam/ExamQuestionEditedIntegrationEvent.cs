namespace Kollity.Services.Contracts.Exam;

public class ExamQuestionEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
}