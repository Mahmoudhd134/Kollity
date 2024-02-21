using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class AssignmentForListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string First150CharFromDescription { get; set; }
    public AssignmentMode Mode { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}