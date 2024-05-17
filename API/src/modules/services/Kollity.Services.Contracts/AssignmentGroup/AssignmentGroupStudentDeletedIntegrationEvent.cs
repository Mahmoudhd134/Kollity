namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupStudentDeletedIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
}