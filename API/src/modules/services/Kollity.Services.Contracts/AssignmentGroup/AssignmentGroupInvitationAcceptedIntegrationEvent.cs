namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupInvitationAcceptedIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
    public Guid RoomId { get; set; }
}