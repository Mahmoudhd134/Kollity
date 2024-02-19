using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos.Assignment.Group;

public class AddAssignmentGroupDto
{
    [Required] public List<Guid> Ids { get; set; }
}