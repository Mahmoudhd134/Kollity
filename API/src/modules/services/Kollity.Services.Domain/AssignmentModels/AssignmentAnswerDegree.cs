using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Domain.AssignmentModels;

public class AssignmentAnswerDegree
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Guid GroupId { get; set; }
    public AssignmentGroup Group { get; set; }
    public Guid AnswerId { get; set; }
    public AssignmentAnswer Answer { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public byte Degree { get; set; }
}