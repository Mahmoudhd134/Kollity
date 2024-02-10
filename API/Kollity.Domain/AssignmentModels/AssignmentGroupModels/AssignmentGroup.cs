using Kollity.Domain.RoomModels;

namespace Kollity.Domain.AssignmentModels.AssignmentGroupModels;

public class AssignmentGroup
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
    public List<AssignmentGroupStudent> AssignmentGroupsStudents { get; set; } = [];
}