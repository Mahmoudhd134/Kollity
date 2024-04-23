namespace Kollity.Services.Contracts;

public class GenericIntegrationEvent
{
    public DateTime EventPublishedDateOnUtc { get; set; } = DateTime.UtcNow;
}