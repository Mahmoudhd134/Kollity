using Domain.AssignmentModels.AssignmentGroupModels;
using Domain.Identity.User;

namespace Domain.AssignmentModels;

public class AssignmentAnswers
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; private set; }
    public DateTime UploadDate { get; set; }
    
    public Guid UserId { get; set; }
    public BaseUser User { get; set; }

    public Guid AssignmentGroupId { get; set; }
    public AssignmentGroup AssignmentGroup { get; set; }
}