namespace Kollity.Exams.Contracts.Exam;

public class ExamAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid RoomId { get; set; }
}