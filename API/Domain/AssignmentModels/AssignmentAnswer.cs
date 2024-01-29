using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.Identity.User;
using Domain.StudentModels;

namespace Domain.AssignmentModels;

public class AssignmentAnswer
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; private set; }
    public DateTime UploadDate { get; set; }
    public string File { get; set; }

    public Guid? StudentId { get; set; }
    public Student Student { get; set; }

    public Guid? AssignmentGroupId { get; set; }
    public AssignmentGroup AssignmentGroup { get; set; }
}