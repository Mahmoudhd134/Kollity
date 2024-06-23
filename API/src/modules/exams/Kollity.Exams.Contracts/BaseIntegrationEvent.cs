namespace Kollity.Exams.Contracts;

public class BaseIntegrationEvent
{
    public DateTime EventPublishedDateOnUtc { get; set; } = DateTime.UtcNow;
}