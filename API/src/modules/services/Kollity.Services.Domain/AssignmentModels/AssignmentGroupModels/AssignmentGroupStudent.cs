using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;

public class AssignmentGroupStudent
{
    public Guid Id { get; set; }
    public Guid AssignmentGroupId { get; set; }

    public AssignmentGroup AssignmentGroup { get; set; }

    // public Guid RoomId { get; set; }
    // public Room Room { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public bool JoinRequestAccepted { get; set; }
}