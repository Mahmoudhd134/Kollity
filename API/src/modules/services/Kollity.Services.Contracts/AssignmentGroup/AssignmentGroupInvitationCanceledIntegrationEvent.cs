namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupInvitationCanceledIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
}