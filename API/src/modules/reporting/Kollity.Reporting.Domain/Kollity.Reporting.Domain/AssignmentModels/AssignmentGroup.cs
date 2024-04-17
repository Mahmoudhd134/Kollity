using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.AssignmentModels;

public class AssignmentGroup
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; }
}