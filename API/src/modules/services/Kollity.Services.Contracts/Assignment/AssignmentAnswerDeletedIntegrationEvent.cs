namespace Kollity.Services.Contracts.Assignment;

public class AssignmentAnswerDeletedIntegrationEvent
{
    public Guid AnswerId { get; set; }
    public Guid AssignmentId { get; set; }
    public List<Guid> StudentIds { get; set; }
    public Guid? GroupId { get; set; }
}