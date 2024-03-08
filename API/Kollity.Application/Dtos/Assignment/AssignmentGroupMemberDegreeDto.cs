namespace Kollity.Application.Dtos.Assignment;

public class AssignmentGroupMemberDegreeDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Code { get; set; }
    public byte? Degree { get; set; }
}