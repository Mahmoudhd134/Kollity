using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class AssignmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AssignmentMode Mode { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public List<AssignmentFileDto> Files { get; set; }
}