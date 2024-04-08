namespace Kollity.Services.Contracts.Assignment;

public class AssignmentDegreeSetIntegrationEvent
{
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
    public byte Degree { get; set; }
}