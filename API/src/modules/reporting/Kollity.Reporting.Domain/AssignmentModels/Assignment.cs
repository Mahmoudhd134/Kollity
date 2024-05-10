using Kollity.Reporting.Domain.RoomModels;
using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.AssignmentModels;

public class Assignment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentMode Mode { get; set; }
    public byte Degree { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public DateTime OpenUntilDate { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public bool IsDeleted { get; set; }

    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
}