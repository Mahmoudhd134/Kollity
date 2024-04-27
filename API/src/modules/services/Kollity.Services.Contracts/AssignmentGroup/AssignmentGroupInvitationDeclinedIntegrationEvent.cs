namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupInvitationDeclinedIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
}