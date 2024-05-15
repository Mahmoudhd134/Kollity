namespace Kollity.Services.Contracts;

public class BaseIntegrationEvent
{
    public DateTime EventPublishedDateOnUtc { get; set; } = DateTime.UtcNow;
}