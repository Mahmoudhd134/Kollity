namespace Kollity.Services.Application.Dtos.Assignment;

public class AssignmentGroupDegreeDto
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public Guid AnswerId { get; set; }
    public List<AssignmentGroupMemberDegreeDto> Members { get; set; }
}