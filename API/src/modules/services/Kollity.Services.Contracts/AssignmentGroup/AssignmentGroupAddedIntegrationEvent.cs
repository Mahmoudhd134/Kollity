namespace Kollity.Services.Contracts.AssignmentGroup;

public class AssignmentGroupAddedIntegrationEvent
{
    public Guid GroupId { get; set; }
    public int Code { get; set; }
    public Guid RoomId { get; set; }
    public List<(Guid id, bool isJoined)> Students { get; set; }
}