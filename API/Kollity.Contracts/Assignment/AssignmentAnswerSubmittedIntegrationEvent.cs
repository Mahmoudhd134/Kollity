namespace Kollity.Contracts.Assignment;

public class AssignmentAnswerSubmittedIntegrationEvent
{
    public Guid AssignmentId { get; set; }
    public List<Guid> StudentIds { get; set; }
    public Guid? GroupId { get; set; }
    public string FilePath { get; set; }
}