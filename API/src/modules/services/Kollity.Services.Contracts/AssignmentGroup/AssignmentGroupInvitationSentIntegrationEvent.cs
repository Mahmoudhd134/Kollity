namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupInvitationSentIntegrationEvent
{
    public Guid GroupId { get; set; }
    public Guid StudentId { get; set; }
    public Guid RoomId { get; set; }
}