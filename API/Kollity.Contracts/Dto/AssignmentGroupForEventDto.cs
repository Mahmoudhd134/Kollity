namespace Kollity.Contracts.Dto;

public class AssignmentGroupForEventDto
{
    public Guid GroupId { get; set; }
    public int Code { get; set; }
    public List<AssignmentGroupMemberForEventDto> Members { get; set; }
}