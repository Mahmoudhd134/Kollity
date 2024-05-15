using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.AssignmentModels;

public class AssignmentAnswer
{
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Guid? RoomId { get; set; }
    public Room Room { get; set; }
    public int? Degree { get; set; }
    public Guid? GroupId { get; set; }
    public AssignmentGroup Group { get; set; }
}