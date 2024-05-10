namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupStudentLeavedIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
}