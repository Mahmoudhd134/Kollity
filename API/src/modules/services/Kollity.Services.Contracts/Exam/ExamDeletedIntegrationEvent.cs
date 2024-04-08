namespace Kollity.Services.Contracts.Exam;

public class ExamDeletedIntegrationEvent
{
    public Guid Id { get; set; }

    public ExamDeletedIntegrationEvent(Guid id)
    {
        Id = id;
    }
}