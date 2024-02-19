namespace Kollity.Application.Dtos.Assignment.Group;

public class AssignmentGroupMemberDto
{
    public Guid Id { get; set; }
    public string ProfileImage { get; set; }
    public string UserName { get; set; }
    public string Code { get; set; }
    public bool IsJoined { get; set; }
}