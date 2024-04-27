namespace Kollity.Services.Contracts.Assignment;

public class AssignmentFileDeletedIntegrationEvent
{
    public Guid AssignmentId { get; set; }
    public Guid FileId { get; set; }
}