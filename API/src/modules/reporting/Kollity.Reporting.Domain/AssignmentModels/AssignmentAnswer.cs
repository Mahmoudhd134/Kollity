using Kollity.Reporting.Domain.UserModels;

namespace Kollity.Reporting.Domain.AssignmentModels;

public class AssignmentAnswer
{
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public int? Degree { get; set; }
    public int? GroupCode { get; set; }
    public AssignmentGroup Group { get; set; }
}