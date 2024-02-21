using Kollity.Domain.DoctorModels;
using Kollity.Domain.RoomModels;

namespace Kollity.Domain.AssignmentModels;

public class Assignment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentMode Mode { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid? DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public List<AssignmentFile> AssignmentFiles { get; set; } = [];
    public List<AssignmentAnswer> AssignmentsAnswers { get; set; } = [];
}