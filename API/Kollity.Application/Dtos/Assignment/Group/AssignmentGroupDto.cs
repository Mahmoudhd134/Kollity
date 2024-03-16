namespace Kollity.Application.Dtos.Assignment.Group;

public class AssignmentGroupDto
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public Guid RoomId { get; set; }
    public List<AssignmentGroupMemberDto> Members { get; set; }
}