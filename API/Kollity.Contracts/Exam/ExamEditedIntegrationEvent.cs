namespace Kollity.Contracts.Exam;

public class ExamEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
}