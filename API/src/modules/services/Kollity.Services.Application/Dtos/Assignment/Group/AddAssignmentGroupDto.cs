using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Assignment.Group;

public class AddAssignmentGroupDto
{
    [Required] public List<Guid> Ids { get; set; }
}