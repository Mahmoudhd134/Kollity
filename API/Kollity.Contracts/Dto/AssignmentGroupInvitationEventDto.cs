namespace Kollity.Contracts.Dto;

public class AssignmentGroupInvitationEventDto
{
    public Guid GroupId { get; set; }
    public int GroupCode { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public string CourseName { get; set; }
    public UserEmailDto UserEmail { get; set; }
}