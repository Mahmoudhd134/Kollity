using Kollity.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Domain.StudentModels;

namespace Kollity.Domain.AssignmentModels;

public class AssignmentAnswer
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; }
    public DateTime UploadDate { get; set; }
    public string File { get; set; }
    public byte? Degree { get; set; }

    public Guid? StudentId { get; set; }
    public Student Student { get; set; }

    public Guid? AssignmentGroupId { get; set; }
    public AssignmentGroup AssignmentGroup { get; set; }
}